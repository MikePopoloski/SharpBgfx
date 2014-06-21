using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    // utility methods for copying managed arrays to native memory
    public static class Memory {
        public static MemoryHandle Copy<T>(T[] array) where T : struct {
            if (array == null || array.Length == 0)
                throw new ArgumentNullException("array");

            var gcHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var memoryHandle = Bgfx.Copy(gcHandle.AddrOfPinnedObject(), Marshal.SizeOf(typeof(T)) * array.Length);

            gcHandle.Free();
            return memoryHandle;
        }
    }
}
