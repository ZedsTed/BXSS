using UnityEngine;

namespace util
{
    public class BeginHorizontal : IControl
    {
        public void Draw()
        {
            GUILayout.BeginHorizontal();
        }
    }

    public class EndHorizontal : IControl
    {
        public void Draw()
        {
            GUILayout.EndHorizontal();
        }
    }

    public class BeginVertical : IControl
    {
        public void Draw()
        {
            GUILayout.BeginVertical();
        }
    }

    public class EndVertical : IControl
    {
        public void Draw()
        {
            GUILayout.EndVertical();
        }
    }
}
