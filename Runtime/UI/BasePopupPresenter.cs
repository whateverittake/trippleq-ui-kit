using System;

namespace TrippleQ.UiKit
{
    public abstract class BasePopupPresenter<TView> where TView : class, ITrippleQPopupView
    {
        protected TView View { get; private set; }
        public bool IsBound => View != null;

        public void Bind(TView view)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));
            if (ReferenceEquals(View, view)) return;

            Unbind(); // safety
            View = view;

            OnBind();
        }

        public void Unbind()
        {
            if (View == null) return;

            OnUnbind();

            // make sure buttons don't keep old closures
            View.SetPrimary(string.Empty, null);
            View.SetSecondary(string.Empty, null);
            View.SetClose(null);
            View = null;
        }

        public void Show()
        {
            EnsureBound();
            OnBeforeShow();
            View.Show();
            OnAfterShow();
        }

        public void Hide()
        {
            if (View == null) return;
            OnBeforeHide();
            View.Hide();
            OnAfterHide();
        }

        protected void EnsureBound()
        {
            if (View == null) throw new InvalidOperationException("Presenter is not bound to a view.");
        }

        protected virtual void OnBind() { }
        protected virtual void OnUnbind() { }

        protected virtual void OnBeforeShow() { }
        protected virtual void OnAfterShow() { }

        protected virtual void OnBeforeHide() { }
        protected virtual void OnAfterHide() { }
    }
}
