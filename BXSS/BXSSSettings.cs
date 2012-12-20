using UnityEngine;

public class BXSSSettings : util.PluginSettings<BXSS>
{
    public BXSSSettings()
    {
        SupersampleAmount = 1;
        ScreenshotKey = "z";
        DisplayKey = "f11";
    }

    public Rect WindowPosition { get; set; }
    public int SupersampleAmount { get; set; }
    public bool AutoHideUI { get; set; }
    public int AutoHideUIDelayInMilliseconds { get; set; }

    public string ScreenshotKey { get; set; }
    public string DisplayKey { get; set; }

    protected override void Validate()
    {
        if (SupersampleAmount <= 0)
            SupersampleAmount = 1;

        if (AutoHideUIDelayInMilliseconds < 0)
            AutoHideUIDelayInMilliseconds = 0;

        if (string.IsNullOrEmpty(ScreenshotKey))
            ScreenshotKey = "z";

        if (string.IsNullOrEmpty(DisplayKey))
            DisplayKey = "f11";
    }
}