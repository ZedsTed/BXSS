namespace util
{
    using System;
    using System.Collections.Generic;

    public class SetButton : Button
    {
        public SetButton()
            : this("Set", new List<ISettable>())
        {
        }

        public SetButton(string text, List<ISettable> settableObjects)
        {
            Text = text;
            SettableObjects = settableObjects;

            base.Clicked = OnClick;
        }

        public new Action Clicked { get; set; }

        private List<ISettable> _settableObjects;
        public List<ISettable> SettableObjects
        {
            get { return _settableObjects; }
            set { ThrowIf.Null(value); _settableObjects = value; }
        }

        private void OnClick()
        {
            foreach (var settableObject in SettableObjects)
                settableObject.Set();

            if(Clicked != null)
                Clicked();
        }
    }
}
