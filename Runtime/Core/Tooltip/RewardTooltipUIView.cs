using System;
using UnityEngine;

namespace TrippleQ.UiKit
{
    public class RewardTooltipUIView : MonoBehaviour
    {
        [SerializeField] RewardTooltipController _goldReward;
        [SerializeField] RewardTooltipController _gemsReward;
        [SerializeField] RewardTooltipController _booster1Reward;
        [SerializeField] RewardTooltipController _booster2Reward;
        [SerializeField] RewardTooltipController _booster3Reward;
        [SerializeField] RewardTooltipController _booster4Reward;

        public void UpdateView(RewardData rewardData)
        {
            if(rewardData.Coin > 0)
            {
                _goldReward.SetCount(rewardData.Coin);
                _goldReward.gameObject.SetActive(true);
            }
            else
            {
                _goldReward.gameObject.SetActive(false);
            }

            if (rewardData.Gem > 0)
            {
                _gemsReward.SetCount(rewardData.Gem);
                _gemsReward.gameObject.SetActive(true);
            }
            else
            {
                _gemsReward.gameObject.SetActive(false);
            }

            if (rewardData.Booster1 > 0)
            {
                _booster1Reward.SetCount(rewardData.Booster1);
                _booster1Reward.gameObject.SetActive(true);
            }
            else
            {
                _booster1Reward.gameObject.SetActive(false);
            }

            if (rewardData.Booster2 > 0)
            {
                _booster2Reward.SetCount(rewardData.Booster2);
                _booster2Reward.gameObject.SetActive(true);
            }
            else
            {
                _booster2Reward.gameObject.SetActive(false);
            }

            if (rewardData.Booster3 > 0)
            {
                _booster3Reward.SetCount(rewardData.Booster3);
                _booster3Reward.gameObject.SetActive(true);
            }
            else
            {
                _booster3Reward.gameObject.SetActive(false);
            }

            if (rewardData.Booster4 > 0)
            {
                _booster4Reward.SetCount(rewardData.Booster4);
                _booster4Reward.gameObject.SetActive(true);
            }
            else
            {
                _booster4Reward.gameObject.SetActive(false);
            }   
        }
    }
}
