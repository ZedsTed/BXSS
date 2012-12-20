using System;
using System.Diagnostics;
using UnityEngine;
using util;

public class Screenshot
{
    private readonly string _screenshotDir;
    private int _screenshotCount;

    private readonly string _timestamp;

    private readonly Stopwatch _stopwatch;
    private readonly Action _disableUI;
    private readonly Action _enableUI;

    private long _autoToggleUIDelay;

    public Screenshot(string screenshotDir)
        : this(screenshotDir, null, null)
    {
    }

    public Screenshot(string screenshotDir, Action disableUI, Action enableUI)
    {
        ThrowIf.NullOrEmpty(screenshotDir, "screenshotDir");

        _screenshotDir = screenshotDir;
        _screenshotCount = 0;
        _timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        _stopwatch = new Stopwatch();
        _disableUI = disableUI;
        _enableUI = enableUI;
    }

    public void Capture(int supersampleAmount)
    {
        Capture(supersampleAmount, false, 0);
    }

    public void Capture(int supersampleAmount, bool autoToggleUI, long autoToggleUIDelay)
    {
        if (supersampleAmount <= 0)
            throw new ArgumentOutOfRangeException("supersampleAmount");

        if (autoToggleUI)
        {
            _autoToggleUIDelay = autoToggleUIDelay;

            _stopwatch.Reset();
            _stopwatch.Start();

            if (_disableUI != null)
                _disableUI();
            RenderingManager.ShowUI(false);
        }

    var fileName = _screenshotDir + _timestamp + "_" + _screenshotCount++ + ".png";
        Application.CaptureScreenshot(fileName, supersampleAmount);
    }

    // Using this instead of timers for simplicity
    public void Update()
    {
        if (!_stopwatch.IsRunning)
            return;

        if (_stopwatch.ElapsedMilliseconds > _autoToggleUIDelay)
        {
            if (_enableUI != null)
                _enableUI();

            RenderingManager.ShowUI(true);
            _stopwatch.Reset();
        }
    }
}