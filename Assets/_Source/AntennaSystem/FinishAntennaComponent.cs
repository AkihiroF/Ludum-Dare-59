using LevelSystem;
using UnityEngine;

namespace AntennaSystem
{
    public class FinishAntennaComponent : AntennaComponent
    {
        [SerializeField] private LevelSwitcher levelSwitcher;
        public override void ReceiveSignalFrom(AntennaComponent from)
        {
            //base.ReceiveSignalFrom(from);
            levelSwitcher.NextLevel();
        }
    }
}