using System.Collections.Generic;

namespace util
{
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
            Clicked = OnClick;
        }

        public List<ISettable> SettableObjects { get; set; } 

        private void OnClick()
        {
            foreach (var settableObject in SettableObjects)
                settableObject.Set();
        }
    }
}
