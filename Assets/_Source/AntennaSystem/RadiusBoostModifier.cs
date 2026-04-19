using AntennaSystem.Data;

namespace AntennaSystem
{
    public class RadiusBoostModifier : IAntennaModifier
    {
        public readonly float Multiplier;

        public RadiusBoostModifier(float multiplier)
        {
            Multiplier = multiplier;
        }

        public float Modify(float baseValue)
        {
            return baseValue * Multiplier;
        }
    }
}