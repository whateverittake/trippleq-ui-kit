using UnityEngine;
using UnityEngine.UI;

namespace TrippleQ.UiKit
{
    public class TooltipController : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private GameObject _layer;      // parent full-screen
        [SerializeField] private Button _blockerButton;  // full-screen transparent button
        [SerializeField] private RectTransform _panel;   // tooltip panel
        [SerializeField] Vector2 _offset= Vector2.zero;

        private Canvas _rootCanvas;     // canvas chứa popup


        private bool _isOpen;

        private void Awake()
        {
            _blockerButton.onClick.AddListener(Close);
            if (_rootCanvas == null)
                _rootCanvas = GetComponentInParent<Canvas>();
            Close();
        }

        public void Toggle(RectTransform anchor, RewardData rewardData)
        {
            if (_isOpen)
            {
                Close();
                return;
            }

            Open(anchor, rewardData);
        }

        public void Open(RectTransform anchor, RewardData rewardData)
        {
            _layer.SetActive(true);
            _isOpen = true;

            _panel.GetComponent<RewardTooltipUIView>().UpdateView(rewardData);
            // OPTIONAL: position panel gần chest
            PositionNearAnchor(anchor);
        }

        public void Close()
        {
            _layer.SetActive(false);
            _isOpen = false;
        }

        private void PositionNearAnchor(RectTransform anchor)
        {
            // Đơn giản: đặt panel trùng vị trí anchor (screen space)
            // Nếu canvas Screen Space - Overlay: anchoredPosition = anchor.anchoredPosition thường ổn
            // Nếu camera/world: cần convert

            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, anchor.position);

            RectTransform canvasRect = (RectTransform)_rootCanvas.transform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out var localPoint);

            _panel.anchoredPosition = localPoint + _offset; // offset xuống dưới 1 chút
        }
    }
}
