
using System;

namespace TrippleQ.UiKit
{
    public interface ITrippleQPopupView
    {
        bool IsVisible { get; }

        // lifecycle UI
        void Show();
        void Hide();
        void SetInteractable(bool interactable);

        // content
        void SetTitle(string title);
        void SetMessage(string message);

        // buttons
        void SetPrimary(string label, Action onClick);
        void SetSecondary(string label, Action onClick);
        void SetClose(Action onClick);

        // optional: loading / busy
        void SetLoading(bool isLoading);
    }
}
