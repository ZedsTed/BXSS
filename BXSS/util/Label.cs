namespace util
{
    using UnityEngine;

    public class Label : AControl
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