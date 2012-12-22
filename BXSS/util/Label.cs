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

        private string _text;
        public string Text
        {
            get { return _text; }
            set { ThrowIf.Null(value); _text = value; }
        }

        protected override void DrawCore()
        {
            BeforeDraw();
            GUILayout.Label(Text ?? "[NULL]", LayoutOptions);
        }

        protected virtual void BeforeDraw()
        {
        }
    }
}