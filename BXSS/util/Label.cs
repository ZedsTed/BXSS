namespace util
{
    using UnityEngine;

    public class Label : AControl
    {
        public Label()
            : this("")
        {
        }

        public Label(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        protected override void DrawCore()
        {
            BeforeDraw();
            GUILayout.Label(Text ?? "", LayoutOptions);
        }

        protected virtual void BeforeDraw()
        {
        }
    }
}