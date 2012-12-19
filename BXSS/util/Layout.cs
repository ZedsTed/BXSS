namespace util
{
    using UnityEngine;

    public class BeginHorizontal : AControl
    {
        public override void Draw()
        {
            GUILayout.BeginHorizontal();
        }
    }

    public class EndHorizontal : AControl
    {
        public override void Draw()
        {
            GUILayout.EndHorizontal();
        }
    }

    public class BeginVertical : AControl
    {
        public override void Draw()
        {
            GUILayout.BeginVertical();
        }
    }

    public class EndVertical : AControl
    {
        public override void Draw()
        {
            GUILayout.EndVertical();
        }
    }
}
