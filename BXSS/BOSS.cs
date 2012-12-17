/*
 * The Boltless Screenshot System (BXSS) is based on the Bolt-On Screenshot System
 * It is provided under the same terms as the original license below
 */

/*
The Bolt-On Screenshot System is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Bolt-On Screenshot System is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with The Bolt-On Screenshot System.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using UnityEngine;
using KSP.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class BOSS : PartModule
{
    public override void OnAwake()
    {
        if (BOSSBehaviour.BOSSBehaviourInstance == null)
            BOSSBehaviour.BOSSBehaviourInstance = GameObject.Find("BOSS") ?? new GameObject("BOSS", typeof(BOSSBehaviour));
    }
}

public class BOSSBehaviour : MonoBehaviour
{
    public static GameObject BOSSBehaviourInstance;

    protected Rect windowPos;
    protected Rect helpWindowPos;
    private string kspDir = KSPUtil.ApplicationRootPath;
    private string kspDir2 = KSPUtil.ApplicationRootPath + @"PluginData/bxss/";
    public int screenshotCount,	superSampleValueInt = 1;
    public string superSampleValueString = "1";
    public string screenshotKey = "f11";
    public bool showHelp = false;
    public int i;
    public bool showGUI = false;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void WindowGUI(int windowID)
    {
        GUIStyle mainGUI = new GUIStyle(GUI.skin.button);
        mainGUI.normal.textColor = mainGUI.focused.textColor = Color.white;
        mainGUI.hover.textColor = mainGUI.active.textColor = Color.yellow;
        mainGUI.onNormal.textColor =
            mainGUI.onFocused.textColor = mainGUI.onHover.textColor = mainGUI.onActive.textColor = Color.green;
        mainGUI.padding = new RectOffset(8, 8, 8, 8);


        GUIStyle infoGUI = new GUIStyle(GUI.skin.button);
        infoGUI.normal.textColor = infoGUI.focused.textColor = Color.white;
        infoGUI.hover.textColor = infoGUI.active.textColor = Color.yellow;
        infoGUI.onNormal.textColor =
            infoGUI.onFocused.textColor = infoGUI.onHover.textColor = infoGUI.onActive.textColor = Color.green;
        infoGUI.padding = new RectOffset(8, 8, 8, 8);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Screenshot", mainGUI, GUILayout.Width(85)))
        {
            screenshotMethod();
        }

        showHelp = GUILayout.Toggle(showHelp, "+", GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    private void helpGUI(int WindowID)
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Current supersample value: " + superSampleValueInt.ToString(), GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        GUILayout.Label("Supersample value: ");
        superSampleValueString = GUILayout.TextField(superSampleValueString);
        try
        {
            superSampleValueInt = Int32.Parse(superSampleValueString);
            i = 0;
        }
        catch
        {
            while (i < 1)
            { // stops the catch from spamming the debug log.
                Debug.Log("You haven't entered an integer.");
                i++;
            }
        }
        GUILayout.Label("You have taken " + screenshotCount + " screenshots.");

        GUILayout.EndVertical();
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    public void screenshotMethod()
    {
        string screenshotFilename =  "Screenshot" + screenshotCount;
        print("Screenshot button pressed!");
        print(screenshotFilename);
        print(screenshotCount);
        print(KSPUtil.ApplicationRootPath);
        print(kspDir);
        print("Your supersample value was " + superSampleValueInt + "!");
        Application.CaptureScreenshot(kspDir2 + screenshotFilename + ".png", superSampleValueInt);
        screenshotCount++;
        saveSettings();
    }

    private void OnGUI()
    {
        if (FlightGlobals.fetch != null && FlightGlobals.ActiveVessel != null)
        {
            GUI.skin = HighLogic.Skin;
            windowPos = GUILayout.Window(569, windowPos, WindowGUI, "B.O.S.S.", GUILayout.Width(120));
            if (showHelp)
                helpWindowPos = GUILayout.Window(568, helpWindowPos, helpGUI, "More Info.", GUILayout.Width(150),
                                                 GUILayout.Height(150));
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            screenshotMethod();
        }
    }

    public Settings settings = new Settings();

    private void saveSettings()
    {
        settings.SetValue("BOSS::screenshotCount", screenshotCount.ToString());
        settings.SetValue("BOSS::windowPos.x", windowPos.x.ToString());
        settings.SetValue("BOSS::windowPos.y", windowPos.y.ToString());
        settings.SetValue("BOSS::helpWindowPos.x", helpWindowPos.x.ToString());
        settings.SetValue("BOSS::helpWindowPos.y", helpWindowPos.y.ToString());
        settings.SetValue("BOSS::showHelp", showHelp.ToString());
        settings.SetValue("BOSS::screenshotKey", screenshotKey);
        settings.SetValue("BOSS::showGUI", showGUI.ToString());
        settings.Save();
        print("Saved BOSS settings.");
    }

    private void loadSettings()
    {
        settings.Load();
        windowPos.x = Convert.ToSingle(settings.GetValue("BOSS::windowPos.x"));
        windowPos.y = Convert.ToSingle(settings.GetValue("BOSS::windowPos.y"));
        helpWindowPos.x = Convert.ToSingle(settings.GetValue("BOSS::helpWindowPos.x"));
        helpWindowPos.y = Convert.ToSingle(settings.GetValue("BOSS::helpWindowPos.y"));
        screenshotCount = Convert.ToInt32(settings.GetValue("BOSS::screenshotCount"));
        showHelp = Convert.ToBoolean(settings.GetValue("BOSS::showHelp"));
        screenshotKey = (settings.GetValue("BOSS::screenshotKey"));
        showGUI = Convert.ToBoolean(settings.GetValue("BOSS::showGUI"));
        print("Loaded BOSS settings.");
    }
}