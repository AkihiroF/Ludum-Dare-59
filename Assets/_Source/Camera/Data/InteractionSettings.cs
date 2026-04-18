using System;
using UnityEngine;

namespace Camera.Data
{
    [Serializable]
    public struct InteractionSettings
    {
        public LayerMask interactionLayer;
        public float distanceForInteract;
    }
}