using System;
using Gameplay;

namespace GameSettings
{
    [Serializable]
    public struct UpgradeData
    {
        public UpgradeType UpgradeType;
        public float BaseValue;
        public float ValueGrowthPower;
        public float BaseCost;
        public float CostGrowthPower;
    }
}