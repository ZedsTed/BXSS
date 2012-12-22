using System.Globalization;

namespace util
{
    using System;
    using UnityEngine;

    // TODO: Add generic argument for value type
    public class TextField<T> : AControl, ISettable
        where T : IConvertible
    {
        private Label _label;
        private string _displayValue;
        private T _value;

        public TextField()
            : this(null, default(T), _ => true)
        {
        }

        public TextField(string caption, T value, Func<T, bool> validator)
        {
            Caption = caption;
            Validator = validator;
            Value = value;
        }

        public T Value
        {
            get { return _value; }
            set
            {
                if (Validator(value))
                {
                    _value = value;
                    _displayValue = _value.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public string Caption
        {
            get { return _label != null ? _label.Text : null; }
            set { _label = new Label(value); }
        }

        public Func<T, bool> Validator { get; set; }

        protected override void DrawCore()
        {
            GUILayout.BeginHorizontal();

            if(!string.IsNullOrEmpty(Caption))
                _label.Draw();

            _displayValue = GUILayout.TextField(_displayValue, LayoutOptions);

            GUILayout.EndHorizontal();
        }

        public void Set()
        {
            T newValue;
            try
            {
                newValue = TypeConvert.Convert<T>(_displayValue);
            }
            catch (NotSupportedException)
            {
                _displayValue = Value.ToString(CultureInfo.InvariantCulture);
                return;
            }

            if (Validator != null)
            {
                if (Validator(newValue))
                    Value = newValue;
            }
            else
                _displayValue = Value.ToString(CultureInfo.InvariantCulture);
        }

        public string Get()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
