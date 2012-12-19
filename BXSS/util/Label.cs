using UnityEngine;

namespace util
{
    public class Label : IControl
    {
        public string Text { get; set; }

        public Label()
            : this("[DEFAULT]")
        {
        }

        public Label(string text)
        {
            Text = text;
        }

        public void Draw()
        {
            GUILayout.Label(Text ?? "[NULL]");
        }
    }
}