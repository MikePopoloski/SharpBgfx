using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBgfx {
    // targets for IL rewriting
    static class RewriteStubs {
        public static unsafe void WriteArray<T>(IntPtr pDest, T[] data, int startIndex, int count) where T : struct {
            throw new NotImplementedException();
        }

        public static unsafe void ReadArray<T>(IntPtr pSrc, T[] data, int startIndex, int count) where T : struct {
            throw new NotImplementedException();
        }

        public static unsafe void WriteInline<T>(void* pDest, ref T srcData) where T : struct {
            throw new NotImplementedException();
        }

        public static unsafe T ReadInline<T>(void* pSrc) where T : struct {
            throw new NotImplementedException();
        }

        public static unsafe int SizeOfInline<T>() {
            throw new NotImplementedException();
        }
    }
}
