using System;
using System.Collections.Generic;

namespace AntennaSystem
{
    public static class SignalConnector
    {
        public static Action<AntennaComponent> CurrentAntennaChanged;
        public static AntennaComponent CurrentAntenna { get; private set; }
        private static List<AntennaComponent> _awailableAntennas;

        public static void SetCurrent(AntennaComponent antenna, List<AntennaComponent> awailableAntennas)
        {
            CurrentAntenna = antenna;
            _awailableAntennas = awailableAntennas;
        }

        public static void TryConnect(AntennaComponent to)
        {
            if(CurrentAntenna is null)
                return;
            if (to == CurrentAntenna)
            {
                CurrentAntenna = null;
                _awailableAntennas = null;
                return;
            }

            if (!_awailableAntennas.Contains(to)) 
                return;
            to.ReceiveSignalFrom(CurrentAntenna);
            CurrentAntenna = null;
            _awailableAntennas = null;
            CurrentAntennaChanged?.Invoke(to);
        }
    }
}