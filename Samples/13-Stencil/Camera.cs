using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimMath;

class Camera {
    Matrix projection;
    Vector3 target;
    Vector3 up;

    public Vector3 Position {
        get;
        set;
    }

    public float VerticalAngle {
        get;
        set;
    }

    public float HorizontalAngle {
        get;
        set;
    }

    public Camera (float fov, int width, int height, float near, float far) {
        projection = Matrix.PerspectiveFovLH(fov * (float)Math.PI / 180.0f, (float)width / height, near, far);

        Position = new Vector3(0.0f, 0.0f, -35.0f);
        target = new Vector3(0.0f, 0.0f, -1.0f);
        up = new Vector3(0.0f, 1.0f, 0.0f);

        HorizontalAngle = 0.01f;
    }

    public Matrix GetViewMatrix () {
        return Matrix.LookAtLH(Position, target, up);
    }
}