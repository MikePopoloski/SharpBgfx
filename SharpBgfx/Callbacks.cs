using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SharpBgfx {
    /// <summary>
    /// Provides an interface for programs to respond to callbacks from the bgfx library.
    /// </summary>
    public interface ICallbackHandler {
        /// <summary>
        /// Called when an error occurs in the library.
        /// </summary>
        /// <param name="errorType">The type of error that occurred.</param>
        /// <param name="message">Message string detailing what went wrong.</param>
        /// <remarks>
        /// If the error type is not <see cref="ErrorType.DebugCheck"/>, bgfx is in an
        /// unrecoverable state and the application should terminate.
        /// 
        /// This method can be called from any thread.
        /// </remarks>
        void ReportError (ErrorType errorType, string message);

        /// <summary>
        /// Queries the size of a cache item.
        /// </summary>
        /// <param name="id">The cache entry ID.</param>
        /// <returns>The size of the cache item, or 0 if the item is not found.</returns>
        int GetCachedSize (long id);

        /// <summary>
        /// Retrieves an entry from the cache.
        /// </summary>
        /// <param name="id">The cache entry ID.</param>
        /// <param name="data">A pointer that should be filled with data from the cache.</param>
        /// <param name="size">The size of the memory block pointed to be <paramref name="data"/>.</param>
        /// <returns><c>true</c> if the item is found in the cache; otherwise, <c>false</c>.</returns>
        bool GetCacheEntry (long id, IntPtr data, int size);

        /// <summary>
        /// Saves an entry in the cache.
        /// </summary>
        /// <param name="id">The cache entry ID.</param>
        /// <param name="data">A pointer to the data to save in the cache.</param>
        /// <param name="size">The size of the memory block pointed to be <paramref name="data"/>.</param>
        void SetCacheEntry (long id, IntPtr data, int size);

        /// <summary>
        /// Save a captured screenshot.
        /// </summary>
        /// <param name="path">The path at which to save the image.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="pitch">The number of bytes between lines in the image.</param>
        /// <param name="data">A pointer to the image data to save.</param>
        /// <param name="size">The size of the image memory.</param>
        /// <param name="flipVertical"><c>true</c> if the image origin is bottom left instead of top left; otherwise, <c>false</c>.</param>
        void SaveScreenShot (string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical);

        /// <summary>
        /// Notifies that a frame capture has begun.
        /// </summary>
        /// <param name="width">The width of the capture surface.</param>
        /// <param name="height">The height of the capture surface.</param>
        /// <param name="pitch">The number of bytes between lines in the captured frames.</param>
        /// <param name="format">The format of captured frames.</param>
        /// <param name="flipVertical"><c>true</c> if the image origin is bottom left instead of top left; otherwise, <c>false</c>.</param>
        void CaptureStarted (int width, int height, int pitch, TextureFormat format, bool flipVertical);

        /// <summary>
        /// Notifies that a frame capture has finished.
        /// </summary>
        void CaptureFinished ();

        /// <summary>
        /// Notifies that a frame has been captured.
        /// </summary>
        /// <param name="data">A pointer to the frame data.</param>
        /// <param name="size">The size of the frame data.</param>
        void CaptureFrame (IntPtr data, int size);
    }

    struct CallbackShim {
        IntPtr unused;
        IntPtr reportError;
        IntPtr getCachedSize;
        IntPtr getCacheEntry;
        IntPtr setCacheEntry;
        IntPtr saveScreenShot;
        IntPtr captureStarted;
        IntPtr captureFinished;
        IntPtr captureFrame;

        public static unsafe IntPtr CreateShim (ICallbackHandler handler) {
            if (handler == null)
                return IntPtr.Zero;

            if (savedDelegates != null)
                throw new InvalidOperationException("Callbacks should only be initialized once; bgfx can only deal with one set at a time.");

            var memory = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CallbackShim)));
            var shim = (CallbackShim*)memory;
            var saver = new DelegateSaver(handler, shim);

            shimMemory = memory;
            savedDelegates = saver;

            return memory;
        }

        public static void FreeShim () {
            if (savedDelegates == null)
                return;

            savedDelegates = null;
            Marshal.FreeHGlobal(shimMemory);
        }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void ReportErrorHandler (ErrorType errorType, string message);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate int GetCachedSizeHandler (long id);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate bool GetCacheEntryHandler (long id, IntPtr data, int size);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void SetCacheEntryHandler (long id, IntPtr data, int size);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void SaveScreenShotHandler (string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void CaptureStartedHandler (int width, int height, int pitch, TextureFormat format, bool flipVertical);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void CaptureFinishedHandler ();

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void CaptureFrameHandler (IntPtr data, int size);

        // We're creating delegates to a user's interface methods; we're then converting those delegates
        // to native pointers and passing them into native code. If we don't save the references to the
        // delegates in managed land somewhere, the GC will think they're unreference and clean them
        // up, leaving native holding a bag of pointers into nowhere land.
        class DelegateSaver {
            ReportErrorHandler reportError;
            GetCachedSizeHandler getCachedSize;
            GetCacheEntryHandler getCacheEntry;
            SetCacheEntryHandler setCacheEntry;
            SaveScreenShotHandler saveScreenShot;
            CaptureStartedHandler captureStarted;
            CaptureFinishedHandler captureFinished;
            CaptureFrameHandler captureFrame;

            public unsafe DelegateSaver (ICallbackHandler handler, CallbackShim* shim) {
                reportError = handler.ReportError;
                getCachedSize = handler.GetCachedSize;
                getCacheEntry = handler.GetCacheEntry;
                setCacheEntry = handler.SetCacheEntry;
                saveScreenShot = handler.SaveScreenShot;
                captureStarted = handler.CaptureStarted;
                captureFinished = handler.CaptureFinished;
                captureFrame = handler.CaptureFrame;

                shim->unused = IntPtr.Zero;
                shim->reportError = Marshal.GetFunctionPointerForDelegate(reportError);
                shim->getCachedSize = Marshal.GetFunctionPointerForDelegate(getCachedSize);
                shim->getCacheEntry = Marshal.GetFunctionPointerForDelegate(getCacheEntry);
                shim->setCacheEntry = Marshal.GetFunctionPointerForDelegate(setCacheEntry);
                shim->saveScreenShot = Marshal.GetFunctionPointerForDelegate(saveScreenShot);
                shim->captureStarted = Marshal.GetFunctionPointerForDelegate(captureStarted);
                shim->captureFinished = Marshal.GetFunctionPointerForDelegate(captureFinished);
                shim->captureFrame = Marshal.GetFunctionPointerForDelegate(captureFrame);
            }
        }

        static IntPtr shimMemory;
        static DelegateSaver savedDelegates;
    }
}
