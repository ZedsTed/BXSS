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

        public GUILayoutOption[] LayoutOptions { get; set; }

        public bool Visible { get; set; }

        public void Draw()
        {
            if (Visible)
                DrawCore();
        }

        protected abstract void DrawCore();
    }
}