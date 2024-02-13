using UnityEngine;
using System;

namespace Main
{
    public static class PlayerResources
    {
        private static PlayerData _playerData;

        private static int _moneyAmountOnLevel;
        public static int GetLevelMoney => _moneyAmountOnLevel;

        public static Action OnPlayerDataChanged;

        public static void SetupData(PlayerData playerData)
        {
            _playerData = playerData;
            OnPlayerDataChanged?.Invoke();
        }

        public static PlayerData GetPlayerData => _playerData;

        public static void SaveData()
        {
            SaveLoadProgress.SaveData(_playerData);
        }

        public static void AddLevelMoney(int amount)
        {
            _moneyAmountOnLevel += amount;
        }

        public static void ResetMoneyOnLevel() => _moneyAmountOnLevel = 0;
    }
}

