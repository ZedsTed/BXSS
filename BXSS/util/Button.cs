using System;
using UnityEngine;

namespace util
{
    public class Button : IControl
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

        public void Draw()
        {
            if (GUILayout.Button(Text))
                Clicked();
        }
    }
}
