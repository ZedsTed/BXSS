namespace util
{
    using UnityEngine;

    public class BeginHorizontal : AControl
    {
        protected override void DrawCore()
        {
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
        protected override void DrawCore()
        {
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
