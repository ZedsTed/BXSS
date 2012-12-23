namespace util
{
    using System;
    using UnityEngine;

    public class ToggleButton : AControl
    {
        private Label _label;

        public ToggleButton()
            : this("", false, null)
        {
        }

        public ToggleButton(string caption, bool value, Action<bool> onToggled)
        {
            Caption = caption;
            Value = value;
            OnToggled = onToggled;
        }

        public string Caption { get; set; }

        public bool Value { get; set; }
        public Action<bool> OnToggled { get; set; }

        protected override void DrawCore()
        {
            var oldValue = Value;
            Value = GUILayout.Toggle(Value, Caption ?? "", GUI.skin.button, LayoutOptions);

            if (oldValue != Value)
                if (OnToggled != null)
                    OnToggled(Value);
        }
    }
}
