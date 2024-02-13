using System.Collections.Generic;

namespace Main
{
    [System.Serializable]
    public struct PlayerData
    {
        public int Level;
        public int MoneyAmount;
        public int SoundSwitcher;
        public int[] BoughtBalloonsIndexes;
        public int CurrentShopSlotIndex;
    }
}


