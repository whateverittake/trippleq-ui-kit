using System;
using TMPro;
using UnityEngine;

namespace TrippleQ.UiKit
{
    public class RewardTooltipController : MonoBehaviour
    {
        const string CountPrefix = "x";

        [SerializeField] TMP_Text _countText;

        public void SetCount(int count)
        {
            _countText.text = CountPrefix + count.ToString();
        }
    }
}
