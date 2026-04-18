using System.Collections.Generic;
using UnityEngine;

namespace AntennaSystem
{
    public static class SignalConnector
    {
        private static AntennaComponent _currentAntenna;
        private static List<AntennaComponent> _awailableAntennas;

        public static void SetCurrent(AntennaComponent antenna, List<AntennaComponent> awailableAntennas)
        {
            _currentAntenna = antenna;
            _awailableAntennas = awailableAntennas;
        }

        public static void TryConnect(AntennaComponent to)
        {
            if(_currentAntenna is null)
                return;
            if (to == _currentAntenna)
            {
                _currentAntenna = null;
                _awailableAntennas = null;
                return;
            }
            if(_awailableAntennas.Contains(to))
            {
                to.ReceiveSignalFrom(_currentAntenna);
                _currentAntenna = null;
                _awailableAntennas = null;
            }
        }
    }
}