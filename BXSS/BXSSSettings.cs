using UnityEngine;

public class BXSSSettings : util.PluginSettings<BXSS>
{
    public BXSSSettings()
    {
        ScreenshotDirectory = "PluginData/BXSS/";
        SupersampleAmount = 1;
        ScreenshotKey = "z";
        DisplayKey = "f11";
    }

    public Rect WindowPosition { get; set; }
    public string ScreenshotDirectory { get; set; }
    public int SupersampleAmount { get; set; }

    public string ScreenshotKey { get; set; }
    public string DisplayKey { get; set; }
}