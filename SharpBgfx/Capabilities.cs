namespace SharpBgfx {
    /// <summary>
    /// Contains information about the capabilities of the rendering device.
    /// </summary>
    public unsafe sealed class Capabilities {
        Caps* data;
        Adapter[] adapters;

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

        /// <summary>
        /// Details about the currently active graphics adapter.
        /// </summary>
        public Adapter CurrentAdapter {
            get { return new Adapter((Vendor)data->VendorId, data->DeviceId); }
        }

        /// <summary>
        /// A list of all graphics adapters installed on the system.
        /// </summary>
        public Adapter[] Adapters {
            get {
                if (adapters == null) {
                    var count = data->GPUCount;
                    adapters = new Adapter[count];
                    for (int i = 0, j = 0; i < count; i++, j += 2)
                        adapters[i] = new Adapter((Vendor)data->GPUs[j], data->GPUs[j + 1]);
                }

                return adapters;
            }
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
            public byte GPUCount;
            public ushort VendorId;
            public ushort DeviceId;

            public fixed ushort GPUs[8];
            public fixed byte Formats[TextureFormatCount];
        }
#pragma warning restore 649
    }

    /// <summary>
    /// Contains details about an installed graphics adapter.
    /// </summary>
    public struct Adapter {
        /// <summary>
        /// Represents the default adapter for the system.
        /// </summary>
        public static readonly Adapter Default = new Adapter(Vendor.None, 0);

        /// <summary>
        /// The IHV that published the adapter.
        /// </summary>
        public readonly Vendor Vendor;

        /// <summary>
        /// A vendor-specific identifier for the adapter type.
        /// </summary>
        public readonly int DeviceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adapter"/> struct.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="deviceId">The device ID.</param>
        public Adapter (Vendor vendor, int deviceId) {
            Vendor = vendor;
            DeviceId = deviceId;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString () => $"Vendor: {Vendor}, Device: {DeviceId}";
    }
}
