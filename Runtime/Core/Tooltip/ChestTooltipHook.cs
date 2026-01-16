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
            _rewardData = new RewardData(coin, gem, booster1, booster2, booster3, booster4);
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

        public RewardData(int coin, int gem,int booster1, int booster2, int booster3, int booster4)
        {
            Coin = coin;
            Gem = gem;
            Booster1 = booster1;
            Booster2 = booster2;
            Booster3 = booster3;
            Booster4 = booster4;
        }
    }
}
