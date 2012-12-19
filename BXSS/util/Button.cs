namespace util
{
    using System;
    using UnityEngine;

    public class Button : AControl
    {
        public string Text { get; set; }

        public Action Clicked { get; set; }

        public Button()
            : this("[DEFAULT]", () => {})
        {
        }

        public Button(string text, Action clicked)
        {
            Text = text;
            Clicked = clicked;
        }

        public override void Draw()
        {
            if (GUILayout.Button(Text, LayoutOptions))
                Clicked();
        }
    }
}
