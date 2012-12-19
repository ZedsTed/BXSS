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

        public string Caption { get; set; }
        public Rect WindowPosition { get; set; }
        public bool Visible { get; set; }
        public bool Draggable { get; set; }

        public List<AControl> Controls { get; set; }

        protected GUILayoutOption[] LayoutOptions { get; set; }

        protected Window()
        {
            _windowID = GetNextWindowID();

            Caption = "[DEFAULT]";
            Visible = true;
            Draggable = true;

            Controls = new List<AControl>();

            LayoutOptions = new[] { GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true) };
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
        public void DrawCallback(int id)
        {
            foreach (var control in Controls)
                control.Draw();

            if (Draggable)
                GUI.DragWindow();
        }
    }
}