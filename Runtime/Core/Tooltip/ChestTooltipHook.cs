using System;
using UnityEngine;

namespace TrippleQ.UiKit
{
    public class ChestTooltipHook : MonoBehaviour
    {
        [SerializeField] private TooltipController _tooltip;
        [SerializeField] private RectTransform _anchor; // chest rect transform
        [SerializeField] GameObject[] _iconObjs;

        private RewardData _rewardData;

        public void InitData(int coin, int gem, int booster1, int booster2, int booster3, int booster4)
        {
            _rewardData = new RewardData();
            _rewardData.Coin = coin;
            _rewardData.Gem = gem;
            _rewardData.Booster1 = booster1;
            _rewardData.Booster2 = booster2;
            _rewardData.Booster3 = booster3;
            _rewardData.Booster4 = booster4;
        }

        public void OnClickChest()
        {
            _tooltip.Toggle(_anchor, _rewardData);
        }
    }

    public class RewardData
    {
        public int Coin;
        public int Gem;
        public int Booster1;
        public int Booster2;
        public int Booster3;
        public int Booster4;
    }
}
