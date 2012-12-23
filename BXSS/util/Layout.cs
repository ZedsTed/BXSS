namespace util
{
    using UnityEngine;

    public class BeginHorizontal : AControl
    {
        private readonly bool _guiGroup;

        public BeginHorizontal()
            : this(false)
        {
        }

        public BeginHorizontal(bool guiGroup)
        {
            _guiGroup = guiGroup;
        }

        protected override void DrawCore()
        {
            if(_guiGroup)
                GUILayout.BeginHorizontal(GUI.skin.textArea);
            else
                GUILayout.BeginHorizontal();
        }
    }

    public class EndHorizontal : AControl
    {
        protected override void DrawCore()
        {
            GUILayout.EndHorizontal();
        }
    }

    public class BeginVertical : AControl
    {
        private readonly bool _guiGroup;

        public BeginVertical()
            : this(false)
        {
        }

        public BeginVertical(bool guiGroup)
        {
            _guiGroup = guiGroup;
        }

        protected override void DrawCore()
        {
            if(_guiGroup)
                GUILayout.BeginVertical(GUI.skin.textArea);
            else
                GUILayout.BeginVertical();
        }
    }

    public class EndVertical : AControl
    {
        protected override void DrawCore()
        {
            GUILayout.EndVertical();
        }
    }
}
/*GUI.skin.textArea*/