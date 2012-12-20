namespace util
{
    using UnityEngine;

    public class Label : AControl
    {
        public Label()
            : this("[DEFAULT]")
        {
        }

        public Label(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        public override void Draw()
        {
            BeforeDraw();
            GUILayout.Label(Text ?? "[NULL]", LayoutOptions);
        }

        protected virtual void BeforeDraw()
        {
        }
    }
}