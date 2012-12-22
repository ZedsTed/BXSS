using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using util;

class BXSSMainWindow : Window
{
    private readonly BXSSSettings _settings;
    private readonly Screenshot _screenshot;

    private readonly List<AControl> _collapsedControls;
    private readonly List<AControl> _expandedControls;

    private readonly GUILayoutOption[] _collapsedLayoutOptions;
    private readonly GUILayoutOption[] _expandedLayoutOptions;

    private bool _collapsed;
    private bool _mainUIEnabled;
    private bool _prevUIState;
    private bool _autoIntervalEnabled;

    private Stopwatch _autoIntervalStopwatch;

    public BXSSMainWindow()
    {
        _settings = new BXSSSettings();
        _settings.Load();
        _screenshot = new Screenshot(
            KSPUtil.ApplicationRootPath + "PluginData/BXSS/",
            () =>
                {
                    _prevUIState = Visible;
                    Visible = false;
                    if(_mainUIEnabled)
                        RenderingManager.ShowUI(false);
                }, 
            () =>
                {
                    Visible = _prevUIState;
                    if(_mainUIEnabled)
                        RenderingManager.ShowUI(true);
                });

        _collapsed = true;
        _mainUIEnabled = true;
        _autoIntervalEnabled = false;

        _autoIntervalStopwatch = new Stopwatch();

        WindowPosition = _settings.WindowPosition;

        Caption = "B.X.S.S";

        var expandButton = new Button {Text = GetCollapsedButtonString(), LayoutOptions = new[] {GUILayout.Width(30)}};
        expandButton.Clicked = () => { _collapsed = !_collapsed; expandButton.Text = GetCollapsedButtonString(); };
        var supersampleAmount = new TextField
                                   {
                                       Value = _settings.SupersampleAmount.ToString(CultureInfo.InvariantCulture),
                                       Caption = "Supersample: ",
                                       Validator = x =>
                                                       {
                                                           int val;
                                                           return int.TryParse(x, out val) && val > 0;
                                                       }
                                   };
        var screenshotButton = new Button
                                   {
                                       Text = "Screenshot",
                                       LayoutOptions = new[] {GUILayout.Width(85)},
                                       Clicked = () => Screenshot()
                                   };

        var toggleAutoHideUI = new Toggle
                                   {
                                       Caption = "Autohide UI: ",
                                       Value = _settings.AutoHideUI,
                                       OnToggled = x =>
                                                       {
                                                           _settings.AutoHideUI = x;
                                                           _settings.Save();
                                                       }
                                   };

        var autoIntervalAmount = new TextField
                                     {
                                         Value = _settings.AutoIntervalDelayInSeconds.ToString(CultureInfo.InvariantCulture),
                                         Visible = false,
                                         Caption = "Auto Interval (s): ",
                                         Validator = x =>
                                                         {
                                                             double val;
                                                             return double.TryParse(x, out val) && val > 0.0;
                                                         }
                                     };

        var toggleAutoInterval = new Toggle
                                     {
                                         Caption = "Auto Interval: ",
                                         Value = _autoIntervalEnabled,
                                         OnToggled = x =>
                                                         {
                                                             _autoIntervalEnabled = !_autoIntervalEnabled;
                                                             autoIntervalAmount.Visible = _autoIntervalEnabled;
                                                         }
                                     };

        var setButton = new SetButton
        {
            LayoutOptions = new[] { GUILayout.Height(25) },
            SettableObjects = new List<ISettable> { supersampleAmount, autoIntervalAmount },
            Clicked = () =>
                          {
                              _settings.SupersampleAmount = int.Parse(supersampleAmount.Value);
                              _settings.AutoIntervalDelayInSeconds = double.Parse(autoIntervalAmount.Value);
                              _settings.Save();
                          }
        };

        _expandedControls = new List<AControl>
                       {
                           new BeginVertical(),
                           new BeginHorizontal(),
                           screenshotButton,
                           expandButton,
                           new EndHorizontal(),
                           toggleAutoHideUI,
                           toggleAutoInterval,
                           supersampleAmount,
                           autoIntervalAmount,
                           setButton,
                           new EndVertical()
                       };

        _collapsedControls = new List<AControl>
                                 {
                                     new BeginHorizontal(),
                                     screenshotButton,
                                     expandButton,
                                     new EndHorizontal()
                                 };

        _expandedLayoutOptions = new[] {GUILayout.Width(180), GUILayout.ExpandHeight(true)};
        _collapsedLayoutOptions = new[]{ GUILayout.Width(120), GUILayout.Height(60) };
    }

    public void OnUpdate()
    {
        _screenshot.Update();

        if (_autoIntervalEnabled)
        {
            if (_autoIntervalStopwatch.Elapsed > TimeSpan.FromSeconds(_settings.AutoIntervalDelayInSeconds))
            {
                _screenshot.Capture(_settings.SupersampleAmount, _settings.AutoHideUI, _settings.AutoHideUIDelayInMilliseconds);

                _autoIntervalStopwatch.Reset();
                _autoIntervalStopwatch.Start();
            }
        }

        if (Input.GetKeyDown(_settings.ScreenshotKey))
            Screenshot();

        if (Input.GetKeyDown(_settings.DisplayKey))
            Visible = !Visible;

        if (FlightGlobals.fetch != null && FlightGlobals.ActiveVessel != null)
            if (Input.GetKeyDown(KeyCode.F2))
                _mainUIEnabled = !_mainUIEnabled;
    }

    protected override void DrawCore()
    {
        Controls = _collapsed ? _collapsedControls : _expandedControls;
        LayoutOptions = _collapsed ? _collapsedLayoutOptions : _expandedLayoutOptions;

        if (WindowPosition != _settings.WindowPosition)
        {
            _settings.WindowPosition = WindowPosition;
            _settings.Save();
        }
    }

    private void Screenshot()
    {
        if (_autoIntervalEnabled)
        {
            if (_autoIntervalStopwatch.IsRunning)
                _autoIntervalStopwatch.Reset();
            else
                _autoIntervalStopwatch.Start();
        }
        else
            _screenshot.Capture(_settings.SupersampleAmount, _settings.AutoHideUI, _settings.AutoHideUIDelayInMilliseconds);
    }

    private string GetCollapsedButtonString()
    {
        return _collapsed ? "+" : "-";
    }
}