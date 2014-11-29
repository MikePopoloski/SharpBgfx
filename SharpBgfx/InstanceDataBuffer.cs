using System;

namespace SharpBgfx {
    public unsafe struct InstanceDataBuffer : IEquatable<InstanceDataBuffer> {
        internal NativeStruct* ptr;

        public IntPtr Data => ptr->data;

        public int Count => ptr->size;

        public InstanceDataBuffer (int count, int stride) {
            ptr = NativeMethods.bgfx_alloc_instance_data_buffer(count, (ushort)stride);
        }

        public static bool CheckAvailableSpace (int count, int stride) {
            return NativeMethods.bgfx_check_avail_instance_data_buffer(count, (ushort)stride);
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance.
        /// </summary>
        /// <param name="other">The object to compare with this instance.</param>
        /// <returns><c>true</c> if the specified object is equal to this instance; otherwise, <c>false</c>.</returns>
        public bool Equals (InstanceDataBuffer other) {
            return ptr == other.ptr;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals (object obj) {
            var other = obj as InstanceDataBuffer?;
            if (other == null)
                return false;

            return Equals(other);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode () {
            return new IntPtr(ptr).GetHashCode();
        }

        /// <summary>
        /// Implements the equality operator.
        /// </summary>
        /// <param name="left">The left side of the operator.</param>
        /// <param name="right">The right side of the operator.</param>
        /// <returns>
        /// <c>true</c> if the two objects are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(InstanceDataBuffer left, InstanceDataBuffer right) {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the inequality operator.
        /// </summary>
        /// <param name="left">The left side of the operator.</param>
        /// <param name="right">The right side of the operator.</param>
        /// <returns>
        /// <c>true</c> if the two objects are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(InstanceDataBuffer left, InstanceDataBuffer right) {
            return !left.Equals(right);
        }

#pragma warning disable 649
        internal struct NativeStruct {
            public IntPtr data;
            public int size;
            public int offset;
            public ushort stride;
            public ushort num;
            public ushort handle;
        }
#pragma warning restore 649
    }
}
