﻿using UnityEngine;

public class BXSSSettings : util.PluginSettings<BXSS>
{
    public BXSSSettings()
    {
        SupersampleAmount = 1;
        ScreenshotKey = KeyCode.Z;
        DisplayKey = KeyCode.F11;
    }

    public Rect WindowPosition { get; set; }
    public int SupersampleAmount { get; set; }
    public bool AutoHideUI { get; set; }
    public int AutoHideUIDelayInMilliseconds { get; set; }
    public int AutoIntervalDelayInSeconds { get; set; }
    public bool EnableOutsideFlight { get; set; }

    public KeyCode ScreenshotKey { get; set; }
    public KeyCode DisplayKey { get; set; }

    protected override void Validate()
    {
        if (SupersampleAmount <= 0)
            SupersampleAmount = 1;

        if (AutoHideUIDelayInMilliseconds < 0)
            AutoHideUIDelayInMilliseconds = 0;

        if (AutoIntervalDelayInSeconds <= 0)
            AutoIntervalDelayInSeconds = 1;
    }
}