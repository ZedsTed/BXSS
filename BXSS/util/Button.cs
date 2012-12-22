namespace util
{
    using System;
    using UnityEngine;

    public class Button : AControl
    {
        public Button()
            : this("[DEFAULT]", null)
        {
        }

        public Button(string text, Action clicked)
        {
            Text = text;
            Clicked = clicked;
        }

        public string Text { get; set; }
        public Action Clicked { get; set; }

        protected override void DrawCore()
        {
            if (GUILayout.Button(Text, LayoutOptions))
                if(Clicked != null)
                    Clicked();
        }
    }
}
