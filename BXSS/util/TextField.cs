namespace util
{
    using System;
    using UnityEngine;

    // TODO: Add generic argument for value type
    public class TextField : AControl, ISettable
    {
        private Label _label;
        private string _displayValue;
        private string _value;

        public TextField()
            : this(null, "[DEFAULT]", _ => true)
        {
        }

        public TextField(string caption, string value, Func<string, bool> validator)
        {
            Caption = caption;
            Validator = validator;
            Value = value;
        }

        public string Value
        {
            get { return _value; }
            set
            {
                if (Validator(value))
                {
                    _value = value;
                    _displayValue = _value;
                }
            }
        }

        public string Caption
        {
            get { return _label != null ? _label.Text : null; }
            set { _label = new Label(value); }
        }

        public Func<string, bool> Validator { get; set; }

        public override void Draw()
        {
            GUILayout.BeginHorizontal();

            if(!string.IsNullOrEmpty(Caption))
                _label.Draw();

            _displayValue = GUILayout.TextField(_displayValue, LayoutOptions);

            GUILayout.EndHorizontal();
        }

        public void Set()
        {
            if (Validator != null)
            {
                if (Validator(_displayValue))
                    Value = _displayValue;
            }

            _displayValue = Value;
        }

        public string Get()
        {
            return Value;
        }
    }
}
