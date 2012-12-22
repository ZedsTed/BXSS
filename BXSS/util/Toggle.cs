namespace util
{
    using System;
    using UnityEngine;

    public class Toggle : AControl
    {
        private Label _label;

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

        public string Caption
        {
            get { return _label != null ? _label.Text : null; }
            set { _label = new Label(value); }
        }

        public bool Value { get; set; }
        public Action<bool> OnToggled { get; set; }

        protected override void DrawCore()
        {
            GUILayout.BeginHorizontal();

            if(!string.IsNullOrEmpty(Caption))
                _label.Draw();

            var oldValue = Value;
            Value = GUILayout.Toggle(Value, GUIContent.none, LayoutOptions);

            GUILayout.EndHorizontal();

            if (oldValue != Value)
                if (OnToggled != null)
                    OnToggled(Value);
        }
    }
}
