using System;

namespace Camera.Data
{
    [Serializable]
    public struct CameraSettings
    {
        public AreaMovement area;
        public float speedMovement;
        public float movementSmooth;
    }
}