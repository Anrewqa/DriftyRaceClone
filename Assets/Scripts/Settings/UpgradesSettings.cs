using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(fileName = "UpgradesSettings", menuName = "Assets/UpgradesSettings", order = 0)]
    public class UpgradesSettings : ScriptableObject
    {
        public static string PathInResources = "UpgradesSettings";
        
        [SerializeField] private UpgradeData _speedData;
        [SerializeField] private UpgradeData _coinRewardData;

        public UpgradeData SpeedData => _speedData;
        public UpgradeData CoinRewardData => _coinRewardData;
    }
}