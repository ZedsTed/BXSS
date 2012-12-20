using System;
using UnityEngine;

namespace util
{
    public class Toggle : AControl
    {
        public bool Value { get; set; }

        public string Caption { get; set; }

        public Action<bool> OnToggled { get; set; }

        public Toggle()
            : this("[DEFAULT]", false, null)
        {
        }

        public Toggle(string caption, bool value, Action<bool> onToggled)
        {
            Caption = caption;
            Value = value;
            OnToggled = onToggled;
        }

        public override void Draw()
        {
            var oldValue = Value;
            Value = string.IsNullOrEmpty(Caption)
                        ? GUILayout.Toggle(Value, GUIContent.none, LayoutOptions)
                        : GUILayout.Toggle(Value, Caption, LayoutOptions);

            if (oldValue != Value)
                if (OnToggled != null)
                    OnToggled(Value);
        }
    }
}
