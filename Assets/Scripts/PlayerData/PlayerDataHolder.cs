using GameSettings;
using UnityEngine;

namespace Gameplay
{
    public static class PlayerDataHolder
    {
        private const string CoinsCountKey = "CoinsCount";
        private const string LastLevelIndexKey = "LastLevelIndex";
        private const string SpeedUpgradeKey = "Speed";
        private const string CoinRewardUpgradeKey = "CoinReward";

        static PlayerDataHolder()
        {
            _settings = Resources.Load<UpgradesSettings>(UpgradesSettings.PathInResources);
            
            CoinsCount = PlayerPrefs.GetInt(CoinsCountKey, 4);
            LevelIndex = PlayerPrefs.GetInt(LastLevelIndexKey, 0);
            SpeedUpgradeIndex = PlayerPrefs.GetInt(SpeedUpgradeKey, 1);
            CoinRewardUpgradeIndex = PlayerPrefs.GetInt(CoinRewardUpgradeKey, 1);

            UpdateSpeed();
            UpdateCoinReward();
        }
        
        private static readonly UpgradesSettings _settings;

        public static int CoinsCount { get; private set; }
        public static int LevelIndex { get; private set; }
        public static float Speed { get; private set; }
        public static int CoinReward { get; private set; }
        public static int SpeedUpgradeIndex{ get; private set; }
        public static int CoinRewardUpgradeIndex{ get; private set; }

        public static void AddCoins()
        {
            CoinsCount += CoinReward;
            PlayerPrefs.SetInt(CoinsCountKey, CoinsCount);
        }
        
        public static void SpendCoins(int count)
        {
            CoinsCount -= CoinReward;
            PlayerPrefs.SetInt(CoinsCountKey, CoinsCount);
        }

        public static void ChangeLastLevelIndex(int index)
        {
            LevelIndex = index;
            PlayerPrefs.SetInt(LastLevelIndexKey, LevelIndex);
        }

        public static void UpdateSpeed()
        {
            Speed = _settings.SpeedData.BaseValue * Mathf.Pow(SpeedUpgradeIndex, _settings.SpeedData.ValueGrowthPower);
        }

        public static void UpdateCoinReward()
        {
            CoinReward = (int) (_settings.CoinRewardData.BaseCost * Mathf.Pow(CoinRewardUpgradeIndex, _settings.CoinRewardData.ValueGrowthPower));
        }

        public static void UpgradeSpeed()
        {
            SpeedUpgradeIndex++;
            PlayerPrefs.SetInt(SpeedUpgradeKey, SpeedUpgradeIndex);
            UpdateSpeed();
        }
        
        public static void UpgradeCoinReward()
        {
            CoinRewardUpgradeIndex++;
            PlayerPrefs.SetInt(CoinRewardUpgradeKey, CoinRewardUpgradeIndex);
            UpdateCoinReward();
        }
    }
}