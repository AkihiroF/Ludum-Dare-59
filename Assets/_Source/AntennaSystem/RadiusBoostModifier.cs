using AntennaSystem.Data;

namespace AntennaSystem
{
    public class RadiusBoostModifier : IAntennaModifier
    {
        private float _multiplier;

        public RadiusBoostModifier(float multiplier)
        {
            _multiplier = multiplier;
        }

        public float Modify(float baseValue)
        {
            return baseValue * _multiplier;
        }
    }
}