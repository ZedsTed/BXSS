namespace util
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Window
    {
        private static int _currentWindowID;

        static Window()
        {
            _currentWindowID = 3505;
        }

        protected static int GetNextWindowID()
        {
            return _currentWindowID++;
        }

        private readonly int _windowID;

        protected Window()
        {
            _windowID = GetNextWindowID();

            Caption = "[DEFAULT]";
            Visible = true;
            Draggable = true;

            Controls = new List<AControl>();

            LayoutOptions = new[] { GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true) };
        }

        protected string Caption { get; set; }

        public Rect WindowPosition { get; set; }
        public bool Visible { get; set; }
        public bool Draggable { get; set; }

        private List<AControl> _controls; 
        public List<AControl> Controls
        {
            get { return _controls; }
            set { ThrowIf.Null(value); _controls = value; }
        }

        private GUILayoutOption[] _layoutOptions;
        protected GUILayoutOption[] LayoutOptions
        {
            get { return _layoutOptions; }
            set { ThrowIf.Null(value); _layoutOptions = value; }
        }

        public void Draw()
        {
            if (!Visible)
                return;

            GUI.skin = HighLogic.Skin;

            DrawCore();

            WindowPosition = GUILayout.Window(
                _windowID,
                WindowPosition,
                DrawCallback,
                string.IsNullOrEmpty(Caption) ? "" : Caption,
                LayoutOptions
                );
        }

        protected abstract void DrawCore();

        // ID parameter needed for callback
        private void DrawCallback(int id)
        {
            foreach (var control in Controls)
                control.Draw();

            if (Draggable)
                GUI.DragWindow();
        }
    }
}