using UnityEngine;

namespace Utils
{
    public static class VectorsExtensions
    {
        public static Vector3 GetDirection(this Vector3 startPoint, Vector3 endPoint, bool isNormalise = true)
            => isNormalise ? (endPoint - startPoint).normalized : endPoint - startPoint;
    }
}