using System;
using Gameplay;
using GameSettings;
using UnityEngine;

namespace UI
{
    public class Upgrades : MonoBehaviour
    {
        private static void UpdateView(UpgradeView view, int value, int cost)
        {
            var coinsCount = PlayerDataHolder.CoinsCount;

            var isAvailable = coinsCount > cost;
            view.UpdateAvailability(isAvailable);
            
            view.UpdateData($"{value}", $"{cost}");
        }
        
        private static (int value, int cost) GetUpgradeData(UpgradeData upgradeData, int index)
        {
            var baseValue = upgradeData.BaseValue;
            var valueGrowthPower = upgradeData.ValueGrowthPower;
            var upgradeValue =  GetUpgradeParameterData(baseValue, valueGrowthPower, index);
            
            var baseCost = upgradeData.BaseCost;
            var costGrowthPower = upgradeData.CostGrowthPower;
            var upgradeCost = GetUpgradeParameterData(baseCost, costGrowthPower, index);

            return (upgradeValue, upgradeCost);
        }

        private static int GetUpgradeParameterData(float parameterBase, float growthFactor, int index)
        {
            return (int) (parameterBase + Mathf.Pow(index, growthFactor));
        }
        
        [SerializeField] private UpgradeView _speedUpgradeView;
        [SerializeField] private UpgradeView _coinRewardUpgradeView;

        private UpgradesSettings _upgradesSettings;

        private void Awake()
        {
            _upgradesSettings = Resources.Load<UpgradesSettings>(UpgradesSettings.PathInResources);
            
            InitializeSpeedUpgradeView();
            InitializeCoinRewardUpgradeView();

            _speedUpgradeView.UpgradeClicked += OnSpeedUpgradeViewClicked;
            _coinRewardUpgradeView.UpgradeClicked += OnCoinRewardUpgradeViewClicked;
        }
        
        private void OnDestroy()
        {
            _speedUpgradeView.UpgradeClicked -= OnSpeedUpgradeViewClicked;
            _coinRewardUpgradeView.UpgradeClicked -= OnCoinRewardUpgradeViewClicked;
        }

        private void OnEnable()
        {
            UpgradeViews();
        }

        private void InitializeSpeedUpgradeView()
        {
            var currentIndex = PlayerDataHolder.SpeedUpgradeIndex;

            var (value, cost) = GetUpgradeData(_upgradesSettings.SpeedData, currentIndex);
            
            _speedUpgradeView.Initialize("Max speed", $"{value}", $"{cost}");
            
            var isAvailable = PlayerDataHolder.CoinsCount > cost;
            _speedUpgradeView.UpdateAvailability(isAvailable);
        }
        
        private void InitializeCoinRewardUpgradeView()
        {
            var currentIndex = PlayerDataHolder.CoinRewardUpgradeIndex;

            var (value, cost) = GetUpgradeData(_upgradesSettings.CoinRewardData, currentIndex);
            
            _coinRewardUpgradeView.Initialize("Gold per coin", $"{value}", $"{cost}");
            
            var isAvailable = PlayerDataHolder.CoinsCount > cost;
            _coinRewardUpgradeView.UpdateAvailability(isAvailable);
        }

        private void OnSpeedUpgradeViewClicked()
        {
            var (value, cost) = GetUpgradeData(_upgradesSettings.SpeedData, PlayerDataHolder.SpeedUpgradeIndex);
            
            PlayerDataHolder.SpendCoins(cost);
            GameEventsManager.CallEvent(GameEventType.CoinSpent);
            PlayerDataHolder.UpgradeSpeed();

            UpgradeViews();
        }
        
        private void OnCoinRewardUpgradeViewClicked()
        {
            var (value, cost) = GetUpgradeData(_upgradesSettings.CoinRewardData, PlayerDataHolder.SpeedUpgradeIndex);
            
            PlayerDataHolder.SpendCoins(cost);
            GameEventsManager.CallEvent(GameEventType.CoinSpent);
            PlayerDataHolder.UpgradeCoinReward();

            UpgradeViews();
        }

        private void UpgradeViews()
        {
            var (value, cost) = GetUpgradeData(_upgradesSettings.SpeedData, PlayerDataHolder.SpeedUpgradeIndex);
            
            UpdateView(_speedUpgradeView, value, cost);
            
            (value, cost) = GetUpgradeData(_upgradesSettings.CoinRewardData, PlayerDataHolder.CoinRewardUpgradeIndex);

            UpdateView(_coinRewardUpgradeView, value, cost);
        }
    }
}