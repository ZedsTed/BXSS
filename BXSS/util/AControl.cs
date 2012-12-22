namespace util
{
    using UnityEngine;

    public abstract class AControl
    {
        protected AControl()
        {
            LayoutOptions = new GUILayoutOption[0];
            Visible = true;
        }

        private GUILayoutOption[] _layoutOptions;
        public GUILayoutOption[] LayoutOptions
        {
            get { return _layoutOptions; }
            set { ThrowIf.Null(value); _layoutOptions = value; }
        }

        public bool Visible { get; set; }

        public void Draw()
        {
            if (Visible)
                DrawCore();
        }

        protected abstract void DrawCore();
    }
}