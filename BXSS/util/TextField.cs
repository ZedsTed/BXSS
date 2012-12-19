using System;
using UnityEngine;

namespace util
{
    public class TextField : IControl, ISettable
    {
        public string Value { get; set; }

        public string Caption
        {
            get { return _label != null ? _label.Text : null; }
            set { _label = new Label(value); }
        }

        public Func<string, bool> Validator { get; set; } 

        private Label _label;
        private string _displayValue;

        public TextField()
            : this(null, "[DEFAULT]", _ => true)
        {
        }

        public TextField(string caption, string value, Func<string, bool> validator)
        {
            Caption = caption;
            Value = value;
            Validator = validator;
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal();

            if(!string.IsNullOrEmpty(Caption))
                _label.Draw();

            _displayValue = GUILayout.TextField(_displayValue);

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
    }
}
