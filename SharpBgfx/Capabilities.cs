using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpBgfx {
    /// <summary>
    /// Contains information about the capabilities of the rendering device.
    /// </summary>
    public unsafe struct Capabilities {
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
        /// The maximum number of draw calls in a single frame.
        /// </summary>
        public int MaxDrawCalls {
            get { return (int)data->MaxDrawCalls; }
        }

        /// <summary>
        /// The maximum number of texture blits in a single frame.
        /// </summary>
        public int MaxBlits {
            get { return (int)data->MaxBlits; }
        }

        /// <summary>
        /// The maximum size of a texture, in pixels.
        /// </summary>
        public int MaxTextureSize {
            get { return (int)data->MaxTextureSize; }
        }

        /// <summary>
        /// The maximum number of render views supported.
        /// </summary>
        public int MaxViews {
            get { return (int)data->MaxViews; }
        }

        /// <summary>
        /// The maximum number of frame buffers that can be allocated.
        /// </summary>
        public int MaxFramebuffers {
            get { return (int)data->MaxFramebuffers; }
        }

        /// <summary>
        /// The maximum number of attachments to a single framebuffer.
        /// </summary>
        public int MaxFramebufferAttachments {
            get { return (int)data->MaxFramebufferAttachements; }
        }

        /// <summary>
        /// The maximum number of programs that can be allocated.
        /// </summary>
        public int MaxPrograms {
            get { return (int)data->MaxPrograms; }
        }

        /// <summary>
        /// The maximum number of shaders that can be allocated.
        /// </summary>
        public int MaxShaders {
            get { return (int)data->MaxShaders; }
        }

        /// <summary>
        /// The maximum number of textures that can be allocated.
        /// </summary>
        public int MaxTextures {
            get { return (int)data->MaxTextures; }
        }

        /// <summary>
        /// The maximum number of texture samplers that can be allocated.
        /// </summary>
        public int MaxTextureSamplers {
            get { return (int)data->MaxTextureSamplers; }
        }

        /// <summary>
        /// The maximum number of vertex declarations that can be allocated.
        /// </summary>
        public int MaxVertexDecls {
            get { return (int)data->MaxVertexDecls; }
        }

        /// <summary>
        /// The maximum number of vertex streams that can be used.
        /// </summary>
        public int MaxVertexStreams {
            get { return (int)data->MaxVertexStreams; }
        }

        /// <summary>
        /// The maximum number of index buffers that can be allocated.
        /// </summary>
        public int MaxIndexBuffers {
            get { return (int)data->MaxIndexBuffers; }
        }

        /// <summary>
        /// The maximum number of vertex buffers that can be allocated.
        /// </summary>
        public int MaxVertexBuffers {
            get { return (int)data->MaxVertexBuffers; }
        }

        /// <summary>
        /// The maximum number of dynamic index buffers that can be allocated.
        /// </summary>
        public int MaxDynamicIndexBuffers {
            get { return (int)data->MaxDynamicIndexBuffers; }
        }

        /// <summary>
        /// The maximum number of dynamic vertex buffers that can be allocated.
        /// </summary>
        public int MaxDynamicVertexBuffers {
            get { return (int)data->MaxDynamicVertexBuffers; }
        }

        /// <summary>
        /// The maximum number of uniforms that can be used.
        /// </summary>
        public int MaxUniforms {
            get { return (int)data->MaxUniforms; }
        }

        /// <summary>
        /// The maximum number of occlusion queries that can be used.
        /// </summary>
        public int MaxOcclusionQueries {
            get { return (int)data->MaxOcclusionQueries; }
        }

        /// <summary>
        /// Indicates whether depth coordinates in NDC range from -1 to 1 (true) or 0 to 1 (false).
        /// </summary>
        public bool HomogeneousDepth {
            get { return data->HomogeneousDepth != 0; }
        }

        /// <summary>
        /// Indicates whether the coordinate system origin is at the bottom left or top left.
        /// </summary>
        public bool OriginBottomLeft {
            get { return data->OriginBottomLeft != 0; }
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
        public AdapterCollection Adapters {
            get { return new AdapterCollection(data->GPUs, data->GPUCount); }
        }

        internal Capabilities (Caps* data) {
            this.data = data;
        }

        /// <summary>
        /// Checks device support for a specific texture format.
        /// </summary>
        /// <param name="format">The format to check.</param>
        /// <returns>The level of support for the given format.</returns>
        public TextureFormatSupport CheckTextureSupport (TextureFormat format) {
            return (TextureFormatSupport)data->Formats[(int)format];
        }

        /// <summary>
        /// Provides access to a collection of adapters.
        /// </summary>
        public unsafe struct AdapterCollection : IReadOnlyList<Adapter> {
            ushort* data;
            int count;

            /// <summary>
            /// Accesses the element at the specified index.
            /// </summary>
            /// <param name="index">The index of the element to retrieve.</param>
            /// <returns>The element at the given index.</returns>
            public Adapter this[int index] {
                get { return new Adapter((Vendor)data[index * 2], data[index * 2 + 1]); }
            }

            /// <summary>
            /// The number of elements in the collection.
            /// </summary>
            public int Count {
                get { return count; }
            }

            internal AdapterCollection (ushort* data, int count) {
                this.data = data;
                this.count = count;
            }

            /// <summary>
            /// Gets an enumerator for the collection.
            /// </summary>
            /// <returns>A collection enumerator.</returns>
            public Enumerator GetEnumerator () {
                return new Enumerator(this);
            }

            IEnumerator<Adapter> IEnumerable<Adapter>.GetEnumerator () {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator () {
                return GetEnumerator();
            }

            /// <summary>
            /// Implements an enumerator for an AdapterCollection.
            /// </summary>
            public struct Enumerator : IEnumerator<Adapter> {
                AdapterCollection collection;
                int index;

                /// <summary>
                /// The current enumerated item.
                /// </summary>
                public Adapter Current {
                    get { return collection[index]; }
                }

                object IEnumerator.Current {
                    get { return Current; }
                }

                internal Enumerator (AdapterCollection collection) {
                    this.collection = collection;
                    index = -1;
                }

                /// <summary>
                /// Advances to the next item in the sequence.
                /// </summary>
                /// <returns><c>true</c> if there are more items in the collection; otherwise, <c>false</c>.</returns>
                public bool MoveNext () {
                    var newIndex = index + 1;
                    if (newIndex >= collection.Count)
                        return false;

                    index = newIndex;
                    return true;
                }

                /// <summary>
                /// Empty; does nothing.
                /// </summary>
                public void Dispose () {
                }

                /// <summary>
                /// Not implemented.
                /// </summary>
                public void Reset () {
                    throw new NotImplementedException();
                }
            }
        }

#pragma warning disable 649
        internal unsafe struct Caps {
            const int TextureFormatCount = 48;

            public RendererBackend Backend;
            public DeviceFeatures Supported;
            public ushort VendorId;
            public ushort DeviceId;
            public byte HomogeneousDepth;
            public byte OriginBottomLeft;
            public byte GPUCount;

            public fixed ushort GPUs[8];

            public uint MaxDrawCalls;
            public uint MaxBlits;
            public uint MaxTextureSize;
            public uint MaxViews;
            public uint MaxFramebuffers;
            public uint MaxFramebufferAttachements;
            public uint MaxPrograms;
            public uint MaxShaders;
            public uint MaxTextures;
            public uint MaxTextureSamplers;
            public uint MaxVertexDecls;
            public uint MaxVertexStreams;
            public uint MaxIndexBuffers;
            public uint MaxVertexBuffers;
            public uint MaxDynamicIndexBuffers;
            public uint MaxDynamicVertexBuffers;
            public uint MaxUniforms;
            public uint MaxOcclusionQueries;

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
        public override string ToString () {
            return string.Format("Vendor: {0}, Device: {0}", Vendor, DeviceId);
        }
    }

    /// <summary>
    /// Contains various performance metrics tracked by the library.
    /// </summary>
    public unsafe struct PerfStats {
        Stats* data;

        /// <summary>
        /// CPU frame start time.
        /// </summary>
        public long CpuTimeStart {
            get { return data->CpuTimeBegin; }
        }

        /// <summary>
        /// CPU frame end time.
        /// </summary>
        public long CpuTimeEnd {
            get { return data->CpuTimeEnd; }
        }

        /// <summary>
        /// CPU timer frequency.
        /// </summary>
        public long CpuTimerFrequency {
            get { return data->CpuTimerFrequency; }
        }

        /// <summary>
        /// Elapsed CPU time.
        /// </summary>
        public TimeSpan CpuElapsed {
            get { return TimeSpan.FromSeconds((double)(CpuTimeEnd - CpuTimeStart) / CpuTimerFrequency); }
        }

        /// <summary>
        /// GPU frame start time.
        /// </summary>
        public long GpuTimeStart {
            get { return data->GpuTimeBegin; }
        }

        /// <summary>
        /// GPU frame end time.
        /// </summary>
        public long GpuTimeEnd {
            get { return data->GpuTimeEnd; }
        }

        /// <summary>
        /// GPU timer frequency.
        /// </summary>
        public long GpuTimerFrequency {
            get { return data->GpuTimerFrequency; }
        }

        /// <summary>
        /// Elapsed GPU time.
        /// </summary>
        public TimeSpan GpuElapsed {
            get { return TimeSpan.FromSeconds((double)(GpuTimeEnd - GpuTimeStart) / GpuTimerFrequency); }
        }

        /// <summary>
        /// Time spent waiting for the render thread.
        /// </summary>
        public long WaitingForRender {
            get { return data->WaitRender; }
        }

        /// <summary>
        /// Time spent waiting for the submit thread.
        /// </summary>
        public long WaitingForSubmit {
            get { return data->WaitSubmit; }
        }

        /// <summary>
        /// The number of draw calls submitted.
        /// </summary>
        public int DrawCallsSubmitted {
            get { return data->NumDraw; }
        }

        /// <summary>
        /// The number of compute calls submitted.
        /// </summary>
        public int ComputeCallsSubmitted {
            get { return data->NumCompute; }
        }

        /// <summary>
        /// Maximum observed GPU driver latency.
        /// </summary>
        public int MaxGpuLatency {
            get { return data->MaxGpuLatency; }
        }

        /// <summary>
        /// The width of the back buffer.
        /// </summary>
        public int BackbufferWidth {
            get { return data->Width; }
        }

        /// <summary>
        /// The height of the back buffer.
        /// </summary>
        public int BackbufferHeight {
            get { return data->Height; }
        }

        /// <summary>
        /// The width of the debug text buffer.
        /// </summary>
        public int TextBufferWidth {
            get { return data->TextWidth; }
        }

        /// <summary>
        /// The height of the debug text buffer.
        /// </summary>
        public int TextBufferHeight {
            get { return data->TextHeight; }
        }

        internal PerfStats (Stats* data) {
            this.data = data;
        }

#pragma warning disable 649
        internal struct Stats {
            public long CpuTimeBegin;
            public long CpuTimeEnd;
            public long CpuTimerFrequency;
            public long GpuTimeBegin;
            public long GpuTimeEnd;
            public long GpuTimerFrequency;
            public long WaitRender;
            public long WaitSubmit;
            public int NumDraw;
            public int NumCompute;
            public int MaxGpuLatency;
            public ushort Width;
            public ushort Height;
            public ushort TextWidth;
            public ushort TextHeight;
        }
#pragma warning restore 649
    }
}
