/*
 * The Boltless Screenshot System (BXSS) is based on the Bolt-On Screenshot System
 * It is provided under the same terms as the original license (GPL v3)
 */

using UnityEngine;

public class BXSS : PartModule
{
    public override void OnAwake()
    {
        if (BXSSBehaviour.BXSSBehaviourInstance == null)
            BXSSBehaviour.BXSSBehaviourInstance = GameObject.Find("BXSS") ?? new GameObject("BXSS", typeof(BXSSBehaviour));
    }
}

public class BXSSBehaviour : MonoBehaviour
{
    public static GameObject BXSSBehaviourInstance;

    private BXSSMainWindow _mainWindow;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        _mainWindow = new BXSSMainWindow();
    }

    public void Update()
    {
        _mainWindow.OnUpdate();
    }

    private void OnGUI()
    {
        _mainWindow.Draw();
    }
}