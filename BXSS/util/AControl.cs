namespace util
{
    using UnityEngine;

    public abstract class AControl
    {
        protected AControl()
        {
            LayoutOptions = new GUILayoutOption[0];
        }

        public GUILayoutOption[] LayoutOptions { get; set; }

        public abstract void Draw();
    }
}