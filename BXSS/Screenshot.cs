using System;
using UnityEngine;
using util;

public class Screenshot
{
    private readonly string _screenshotDir;
    private int _screenshotCount;

    private readonly string _timestamp;

    public Screenshot(string screenshotDir)
    {
        ThrowIf.NullOrEmpty(screenshotDir, "screenshotDir");

        _screenshotDir = screenshotDir;
        _timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        _screenshotCount = 0;
    }

    public void Capture(int supersampleAmount)
    {
        if(supersampleAmount <= 0)
            throw new ArgumentOutOfRangeException("supersampleAmount");

        var fileName = _screenshotDir + _timestamp + "_" + _screenshotCount++ + ".png";
        Application.CaptureScreenshot(fileName, supersampleAmount);
    }
}