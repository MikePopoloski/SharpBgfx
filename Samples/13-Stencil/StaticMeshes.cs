using System;
using Common;

static class StaticMeshes {
    public static readonly PosNormalTexcoordVertex[] HorizontalPlane = {
        new PosNormalTexcoordVertex(-1.0f, 0.0f,  1.0f, PackF4u(0.0f, 1.0f, 0.0f), 5.0f, 5.0f),
        new PosNormalTexcoordVertex( 1.0f, 0.0f,  1.0f, PackF4u(0.0f, 1.0f, 0.0f), 5.0f, 0.0f),
        new PosNormalTexcoordVertex(-1.0f, 0.0f, -1.0f, PackF4u(0.0f, 1.0f, 0.0f), 0.0f, 5.0f),
        new PosNormalTexcoordVertex( 1.0f, 0.0f, -1.0f, PackF4u(0.0f, 1.0f, 0.0f), 0.0f, 0.0f)
    };

    public static readonly PosNormalTexcoordVertex[] VerticalPlane = {
        new PosNormalTexcoordVertex(-1.0f,  1.0f, 0.0f, PackF4u(0.0f, 0.0f, -1.0f), 1.0f, 1.0f),
        new PosNormalTexcoordVertex( 1.0f,  1.0f, 0.0f, PackF4u(0.0f, 0.0f, -1.0f), 1.0f, 0.0f),
        new PosNormalTexcoordVertex(-1.0f, -1.0f, 0.0f, PackF4u(0.0f, 0.0f, -1.0f), 0.0f, 1.0f),
        new PosNormalTexcoordVertex( 1.0f, -1.0f, 0.0f, PackF4u(0.0f, 0.0f, -1.0f), 0.0f, 0.0f)
    };

    public static readonly ushort[] PlaneIndices = {
        0, 1, 2,
        1, 3, 2
    };

    static int PackF4u (float x, float y, float z) {
        var bytes = new byte[] {
            (byte)(x * 127.0f + 128.0f),
            (byte)(y * 127.0f + 128.0f),
            (byte)(z * 127.0f + 128.0f),
            128
        };

        return BitConverter.ToInt32(bytes, 0);
    }
}