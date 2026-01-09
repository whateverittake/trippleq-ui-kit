using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TrippleQ.UiKit
{
    /// <summary>
    /// Simple Tab Controller:
    /// - Each tab has: Button + Content panel
    /// - Optional: SelectedVisual / DeselectedVisual GameObjects for state styling
    /// - Selecting a tab: activates its content, sets visuals accordingly
    /// </summary>
    public sealed class TabController : MonoBehaviour
    {
        [Serializable]
        public sealed class Tab
        {
            public string id;

            [Header("Core")]
            public Button button;
            public GameObject content;

            [Header("State Visuals")]
            public GameObject selectedVisual;     // shown when selected
            public GameObject deselectedVisual;   // shown when NOT selected

            [Header("Optional")]
            public bool interactable = true;
        }

        [Header("Tabs")]
        [SerializeField] private List<Tab> _tabs = new List<Tab>();

        [Header("Config")]
        [SerializeField] private int _defaultIndex = 0;
        [SerializeField] private bool _selectDefaultOnEnable = true;

        public event Action<int, Tab> OnTabChanged;

        public int CurrentIndex { get; private set; } = -1;
        public Tab CurrentTab => (CurrentIndex >= 0 && CurrentIndex < _tabs.Count) ? _tabs[CurrentIndex] : null;

        private void Awake()
        {
            WireButtons();
        }

        private void OnEnable()
        {
            if (_selectDefaultOnEnable)
            {
                var idx = Mathf.Clamp(_defaultIndex, 0, Mathf.Max(0, _tabs.Count - 1));
                Select(idx, notify: false);
            }
            else
            {
                Refresh();
            }
        }

        private void OnDestroy()
        {
            UnwireButtons();
        }

        public void Select(int index, bool notify = true)
        {
            if (_tabs == null || _tabs.Count == 0) return;
            if (index < 0 || index >= _tabs.Count) return;
            if (!_tabs[index].interactable) return;

            if (CurrentIndex == index)
            {
                Refresh();
                return;
            }

            CurrentIndex = index;
            Refresh();

            if (notify)
                OnTabChanged?.Invoke(CurrentIndex, _tabs[CurrentIndex]);
        }

        public bool Select(string id, bool notify = true)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;

            for (int i = 0; i < _tabs.Count; i++)
            {
                if (string.Equals(_tabs[i].id, id, StringComparison.Ordinal))
                {
                    if (!_tabs[i].interactable) return false;
                    Select(i, notify);
                    return true;
                }
            }

            return false;
        }

        public void SetTabInteractable(int index, bool interactable)
        {
            if (_tabs == null || index < 0 || index >= _tabs.Count) return;

            _tabs[index].interactable = interactable;

            var t = _tabs[index];
            if (t.button != null) t.button.interactable = interactable;

            // If disabling the currently selected tab, select the first enabled
            if (!interactable && CurrentIndex == index)
            {
                var fallback = FindFirstEnabledIndex();
                if (fallback >= 0) Select(fallback, notify: true);
                else
                {
                    CurrentIndex = -1;
                    Refresh();
                }
            }
            else
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            for (int i = 0; i < _tabs.Count; i++)
            {
                var selected = (i == CurrentIndex);
                ApplyState(_tabs[i], selected);
            }
        }

        private void WireButtons()
        {
            for (int i = 0; i < _tabs.Count; i++)
            {
                int idx = i;
                var t = _tabs[i];
                if (t.button == null) continue;

                // Ensure we don't capture a different closure each time:
                t.button.onClick.RemoveAllListeners();
                t.button.onClick.AddListener(() => Select(idx));

                t.button.interactable = t.interactable;
            }
        }

        private void UnwireButtons()
        {
            for (int i = 0; i < _tabs.Count; i++)
            {
                var t = _tabs[i];
                if (t.button == null) continue;
                t.button.onClick.RemoveAllListeners();
            }
        }

        private void ApplyState(Tab tab, bool selected)
        {
            // Content
            if (tab.content != null)
                tab.content.SetActive(selected);

            // Button enabled/disabled
            if (tab.button != null)
                tab.button.interactable = tab.interactable;

            // Visuals
            if (tab.selectedVisual != null)
                tab.selectedVisual.SetActive(selected);

            if (tab.deselectedVisual != null)
                tab.deselectedVisual.SetActive(!selected);
        }

        private int FindFirstEnabledIndex()
        {
            for (int i = 0; i < _tabs.Count; i++)
                if (_tabs[i].interactable) return i;
            return -1;
        }
    }
}
