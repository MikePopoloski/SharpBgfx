using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Pdb;

namespace ILRewriter {
    /// <summary>
    /// Tool that patches a managed assembly to use specific IL instructions not available from normal C# code.
    /// </summary>
    class Program {
        static AssemblyDefinition mscorLib;

        static void Main (string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("No arguments passed.");
                return;
            }

            var filePath = args[0];
            filePath = Path.GetFullPath(filePath);

            if (!File.Exists(filePath)) {
                Console.WriteLine("Target path does not exist.");
                return;
            }

            string keyFilePath = null;
            if (args.Length > 1) {
                keyFilePath = args[1];
                keyFilePath = Path.GetFullPath(keyFilePath);
                if (!File.Exists(keyFilePath))
                    keyFilePath = null;
            }

            GenerateInterop(filePath, keyFilePath);
        }

        static void GenerateInterop (string filePath, string keyFilePath) {
            var pdbFile = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".pdb");

            var readerParams = new ReaderParameters();
            var writerParams = new WriterParameters();

            if (keyFilePath != null)
                writerParams.StrongNameKeyPair = new StrongNameKeyPair(File.Open(keyFilePath, FileMode.Open));

            if (File.Exists(pdbFile)) {
                readerParams.SymbolReaderProvider = new PdbReaderProvider();
                readerParams.ReadSymbols = true;
                writerParams.WriteSymbols = true;
            }

            var assemblyDef = AssemblyDefinition.ReadAssembly(filePath, readerParams);
            ((BaseAssemblyResolver)assemblyDef.MainModule.AssemblyResolver).AddSearchDirectory(Path.GetDirectoryName(filePath));

            foreach (var assemblyNameReference in assemblyDef.MainModule.AssemblyReferences) {
                if (assemblyNameReference.Name.ToLower() == "mscorlib") {
                    mscorLib = assemblyDef.MainModule.AssemblyResolver.Resolve(assemblyNameReference);
                    break;
                }
            }

            if (mscorLib == null)
                throw new InvalidOperationException("Missing mscorlib.dll");

            for (int i = 0; i < assemblyDef.CustomAttributes.Count; i++) {
                var attr = assemblyDef.CustomAttributes[i];
                if (attr.AttributeType.FullName == typeof(CompilationRelaxationsAttribute).FullName) {
                    assemblyDef.CustomAttributes.RemoveAt(i);
                    i--;
                }
            }

            foreach (var typeDef in assemblyDef.MainModule.Types)
                PatchType(typeDef);

            RemoveStubClass(assemblyDef);
            assemblyDef.Write(filePath, writerParams);
        }

        static string GetProgramFilesFolder () {
            if (IntPtr.Size == 8 || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        static void PatchType (TypeDefinition typeDef) {
            foreach (var method in typeDef.Methods)
                PatchMethod(method);

            foreach (var nestedTypeDef in typeDef.NestedTypes)
                PatchType(nestedTypeDef);
        }

        static void PatchMethod (MethodDefinition method) {
            if (method.HasBody) {
                var ilGen = method.Body.GetILProcessor();
                var instructions = method.Body.Instructions;

                for (int i = 0; i < instructions.Count; i++) {
                    var currInstruction = instructions[i];
                    if (currInstruction.OpCode == OpCodes.Call && currInstruction.Operand is MethodReference) {
                        var methodDescr = (MethodReference)currInstruction.Operand;
                        if (methodDescr.DeclaringType.Name.Equals("RewriteStubs")) {
                            if (methodDescr.Name.StartsWith("SizeOfInline"))
                                ReplaceSizeOfStructGeneric(methodDescr, ilGen, currInstruction);
                            else if (methodDescr.Name.StartsWith("ReadInline"))
                                ReplaceReadInline(methodDescr, ilGen, currInstruction);
                            else if (methodDescr.Name.StartsWith("WriteInline"))
                                ReplaceWriteInline(methodDescr, ilGen, currInstruction);
                            else if (methodDescr.Name.StartsWith("WriteArray")) {
                                CreateWriteArrayMethod(method);
                                break;
                            }
                            else if (methodDescr.Name.StartsWith("ReadArray")) {
                                CreateReadArrayMethod(method);
                                break;
                            }
                        }
                    }
                }
            }
        }

        static void ReplaceReadInline (MethodReference methodDescr, ILProcessor ilGen, Instruction instructionToPatch) {
            var paramT = ((GenericInstanceMethod)instructionToPatch.Operand).GenericArguments[0];
            var newInstruction = ilGen.Create(OpCodes.Ldobj, paramT);
            ilGen.Replace(instructionToPatch, newInstruction);
        }

        static void ReplaceWriteInline (MethodReference methodDescr, ILProcessor ilGen, Instruction instructionToPatch) {
            var paramT = ((GenericInstanceMethod)instructionToPatch.Operand).GenericArguments[0];
            var newInstruction = ilGen.Create(OpCodes.Cpobj, paramT);
            ilGen.Replace(instructionToPatch, newInstruction);
        }

        static void ReplaceSizeOfStructGeneric (MethodReference methodDescr, ILProcessor ilGen, Instruction instructionToPatch) {
            var paramT = ((GenericInstanceMethod)instructionToPatch.Operand).GenericArguments[0];
            var newInstruction = ilGen.Create(OpCodes.Sizeof, paramT);
            ilGen.Replace(instructionToPatch, newInstruction);
        }

        static void Inject (ILProcessor ilGen, IEnumerable<Instruction> instructions, Instruction instructionToReplace) {
            var prevInstruction = instructionToReplace;
            foreach (var currInstruction in instructions) {
                ilGen.InsertAfter(prevInstruction, currInstruction);
                prevInstruction = currInstruction;
            }

            ilGen.Remove(instructionToReplace);
        }

        static void RemoveStubClass (AssemblyDefinition assemblyDef) {
            var interopType = assemblyDef.MainModule.Types.FirstOrDefault(t => t.Name.StartsWith("RewriteStubs"));
            if (interopType != null)
                assemblyDef.MainModule.Types.Remove(interopType);
        }

        static void CreateWriteArrayMethod (MethodDefinition method) {
            var opExplicitInfo = typeof(IntPtr).GetMethod("op_Explicit", new[] { typeof(void*) });
            var opExplicitRef = method.Module.Import(opExplicitInfo);

            method.Body.Instructions.Clear();
            method.Body.InitLocals = true;

            var ilGen = method.Body.GetILProcessor();
            var paramT = method.GenericParameters[0];

            method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(paramT))));

            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Call, opExplicitRef);
            ilGen.Emit(OpCodes.Ldarg_1);
            ilGen.Emit(OpCodes.Ldarg_2);
            ilGen.Emit(OpCodes.Ldelema, paramT);
            ilGen.Emit(OpCodes.Stloc_0);
            ilGen.Emit(OpCodes.Ldloc_0);
            ilGen.Emit(OpCodes.Sizeof, paramT);
            ilGen.Emit(OpCodes.Conv_I4);
            ilGen.Emit(OpCodes.Ldarg_3);
            ilGen.Emit(OpCodes.Mul);
            ilGen.Emit(OpCodes.Unaligned, (byte)1);
            ilGen.Emit(OpCodes.Cpblk);
            ilGen.Emit(OpCodes.Ret);
        }

        static void CreateReadArrayMethod (MethodDefinition method) {
            var opExplicitInfo = typeof(IntPtr).GetMethod("op_Explicit", new[] { typeof(void*) });
            var opExplicitRef = method.Module.Import(opExplicitInfo);

            method.Body.Instructions.Clear();
            method.Body.InitLocals = true;

            var ilGen = method.Body.GetILProcessor();
            var paramT = method.GenericParameters[0];

            method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(paramT))));

            ilGen.Emit(OpCodes.Ldarg_1);
            ilGen.Emit(OpCodes.Ldarg_2);
            ilGen.Emit(OpCodes.Ldelema, paramT);
            ilGen.Emit(OpCodes.Stloc_0);
            ilGen.Emit(OpCodes.Ldloc_0);
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Call, opExplicitRef);
            ilGen.Emit(OpCodes.Sizeof, paramT);
            ilGen.Emit(OpCodes.Conv_I4);
            ilGen.Emit(OpCodes.Ldarg_3);
            ilGen.Emit(OpCodes.Mul);
            ilGen.Emit(OpCodes.Unaligned, (byte)1);
            ilGen.Emit(OpCodes.Cpblk);
            ilGen.Emit(OpCodes.Ret);
        }
    }
}
