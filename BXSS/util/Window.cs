using System.Collections.Generic;
using UnityEngine;

namespace util
{
    public class Window
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
        public Rect WindowRect { get; set; }
        public bool Visible { get; set; }
        public bool Draggable { get; set; }

        public List<IControl> Controls { get; set; }

        public Window()
        {
            _windowID = GetNextWindowID();

            Caption = "[DEFAULT]";
            Visible = true;
            Draggable = true;

            Controls = new List<IControl>();
        }

        // ID parameter needed for callback
        public void Draw(int id)
        {
            if (!Visible)
                return;

            foreach(var control in Controls)
                control.Draw();

            if(Draggable)
                GUI.DragWindow();
        }
    }
}
