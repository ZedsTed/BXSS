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

    private BXSSSettings _settings;
    private Screenshot _screenshot;
    private BXSSMainWindow _mainWindow;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        _settings = new BXSSSettings();
        _settings.Load();

        _screenshot = new Screenshot(KSPUtil.ApplicationRootPath + "PluginData/BXSS/", () => _mainWindow.Visible = false, () => _mainWindow.Visible = true);
        _mainWindow = new BXSSMainWindow(_settings, _screenshot);
    }

    public void Update()
    {
        _screenshot.Update();

        if (Input.GetKeyDown(_settings.ScreenshotKey))
            _screenshot.Capture(_settings.SupersampleAmount, _settings.AutoHideUI, _settings.AutoHideUIDelayInMilliseconds);

        if (Input.GetKeyDown(_settings.DisplayKey))
            _mainWindow.Visible = !_mainWindow.Visible;
    }

    private void OnGUI()
    {
        _mainWindow.Draw();
    }
}