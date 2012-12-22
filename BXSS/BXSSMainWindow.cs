using System.Collections.Generic;
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

        WindowPosition = _settings.WindowPosition;

        Caption = "B.X.S.S";

        var expandButton = new Button {Text = GetCollapsedButtonString(), LayoutOptions = new[] {GUILayout.Width(30)}};
        expandButton.Clicked = () => { _collapsed = !_collapsed; expandButton.Text = GetCollapsedButtonString(); };
        var superSampleField = new TextField
                                   {
                                       Value = _settings.SupersampleAmount.ToString(CultureInfo.InvariantCulture),
                                       Caption = "Supersample: ",
                                       Validator = x =>
                                                       {
                                                           int val;
                                                           return int.TryParse(x, out val) && val > 0;
                                                       }
                                   };
        var setButton = new SetButton
                            {
                                LayoutOptions = new[] {GUILayout.Height(25)},
                                SettableObjects = new List<ISettable> {superSampleField},
                                Clicked = () =>
                                              {
                                                  if (_settings.SupersampleAmount != int.Parse(superSampleField.Value))
                                                  {
                                                      _settings.SupersampleAmount = int.Parse(superSampleField.Value);
                                                      _settings.Save();
                                                  }
                                              }
                            };
        var screenshotButton = new Button {Text = "Screenshot", Clicked = () => _screenshot.Capture(_settings.SupersampleAmount, _settings.AutoHideUI, _settings.AutoHideUIDelayInMilliseconds), LayoutOptions = new[] {GUILayout.Width(85)}};

        var toggleAutoHideUI = new Toggle
                                   {
                                       Caption = "Autohide UI",
                                       Value = _settings.AutoHideUI,
                                       OnToggled = x =>
                                                       {
                                                           _settings.AutoHideUI = x;
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
                           new BeginHorizontal(),
                           superSampleField,
                           setButton,
                           new EndHorizontal(),
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

        if (Input.GetKeyDown(_settings.ScreenshotKey))
            _screenshot.Capture(_settings.SupersampleAmount, _settings.AutoHideUI, _settings.AutoHideUIDelayInMilliseconds);

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

    private string GetCollapsedButtonString()
    {
        return _collapsed ? "+" : "-";
    }
}