using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe struct Uniform : IDisposable {
        internal ushort handle;

        public Uniform (string name, UniformType type, int arraySize = 1) {
            handle = bgfx_create_uniform(name, type, (ushort)arraySize);
        }

        public void Dispose () {
            bgfx_destroy_uniform(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_uniform ([MarshalAs(UnmanagedType.LPStr)] string name, UniformType type, ushort arraySize);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_uniform (ushort handle);
    }
}
