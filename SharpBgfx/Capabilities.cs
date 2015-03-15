namespace SharpBgfx {
    /// <summary>
    /// Contains information about the capabilities of the rendering device.
    /// </summary>
    public unsafe sealed class Capabilities {
        Caps* data;

        /// <summary>
        /// The currently active rendering backend API.
        /// </summary>
        public RendererBackend Backend {
            get { return data->Backend; }
        }

        /// <summary>
        /// A set of extended features supported by the device.
        /// </summary>
        public DeviceFeatures SupportedFeatures {
            get { return data->Supported; }
        }

        /// <summary>
        /// The maximum size of a texture, in pixels.
        /// </summary>
        public int MaxTextureSize {
            get { return data->MaxTextureSize; }
        }

        /// <summary>
        /// The maximum number of render views supported.
        /// </summary>
        public int MaxViews {
            get { return data->MaxViews; }
        }

        /// <summary>
        /// The maximum number of draw calls in a single frame.
        /// </summary>
        public int MaxDrawCalls {
            get { return data->MaxDrawCalls; }
        }

        /// <summary>
        /// The maximum number of attachments to a single framebuffer.
        /// </summary>
        public int MaxFramebufferAttachments {
            get { return data->MaxFramebufferAttachements; }
        }

        internal Capabilities () {
            data = NativeMethods.bgfx_get_caps();
        }

        /// <summary>
        /// Checks device support for a specific texture format.
        /// </summary>
        /// <param name="format">The format to check.</param>
        /// <returns>The level of support for the given format.</returns>
        public TextureFormatSupport CheckTextureSupport (TextureFormat format) {
            return (TextureFormatSupport)data->Formats[(int)format];
        }

#pragma warning disable 649
        internal unsafe struct Caps {
            const int TextureFormatCount = 48;

            public RendererBackend Backend;
            public DeviceFeatures Supported;
            public ushort MaxTextureSize;
            public ushort MaxViews;
            public ushort MaxDrawCalls;
            public byte MaxFramebufferAttachements;

            public fixed byte Formats[TextureFormatCount];
        }
#pragma warning restore 649
    }
}
