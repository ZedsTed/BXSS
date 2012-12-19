using UnityEngine;

namespace util
{
    public abstract class AControl
    {
        protected AControl()
        {
            LayoutOptions = new GUILayoutOption[0];
        }

        public abstract void Draw();

        public GUILayoutOption[] LayoutOptions { get; set; }
    }
}