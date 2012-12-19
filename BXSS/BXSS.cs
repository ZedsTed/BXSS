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

public class BXSS : PartModule
{
    public override void OnAwake()
    {
        if (BXSSBehaviour.BOSSBehaviourInstance == null)
            BXSSBehaviour.BOSSBehaviourInstance = GameObject.Find("BOSS") ?? new GameObject("BOSS", typeof(BXSSBehaviour));
    }
}

public class BXSSBehaviour : MonoBehaviour
{
    public static GameObject BOSSBehaviourInstance;

    protected Rect windowPos;
    protected Rect helpWindowPos;
    private string kspDir = KSPUtil.ApplicationRootPath;
    private string kspDir2 = KSPUtil.ApplicationRootPath + @"PluginData/BXSS/";
    public int screenshotCount,	superSampleValueInt = 1;
    public string superSampleValueString = "1";
    public string screenshotKey = "z";
    public string displayKey = "f11";
    public bool display = false;
    public bool showHelp = false;
    public int i;
    public bool showGUI = false;

    public void Awake()
    {
        loadSettings();
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

        if (GUILayout.Button(showHelp ? "-" : "+", mainGUI, GUILayout.Width(30)))
            showHelp = !showHelp;
         
        GUILayout.EndHorizontal();

        if (showHelp)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Current supersample value: " + superSampleValueInt.ToString(), GUILayout.ExpandHeight(true),
                            GUILayout.ExpandWidth(true));
            GUILayout.Label("Supersample value: ");
            superSampleValueString = GUILayout.TextField(superSampleValueString);

            int newSuperSampleValue;
            if(int.TryParse(superSampleValueString, out newSuperSampleValue) && newSuperSampleValue >= 1)
            {
                superSampleValueInt = newSuperSampleValue;
                superSampleValueString = superSampleValueInt.ToString();
            }

            GUILayout.Label("You have taken " + screenshotCount + " screenshots.");

            GUILayout.EndVertical();
        }
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    public void screenshotMethod()
    {
        string screenshotFilename =  "Screenshot" + screenshotCount;
        Application.CaptureScreenshot(kspDir2 + screenshotFilename + ".png", superSampleValueInt);
        screenshotCount++;
        saveSettings();
    }

    private void OnGUI()
    {
       // if (FlightGlobals.fetch != null && FlightGlobals.ActiveVessel != null
        if(display)
        {
            GUI.skin = HighLogic.Skin;
            windowPos = GUILayout.Window(569, windowPos, WindowGUI, "B.X.S.S.",
                                         showHelp
                                             ? new[] {GUILayout.Width(180), GUILayout.ExpandHeight(true)}
                                             : new[] {GUILayout.Width(120), GUILayout.Height(60)});
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
            screenshotMethod();

        if (Input.GetKeyDown(displayKey))
            display = !display;
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
        settings.SetValue("BOSS::displayKey", displayKey);
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
        displayKey = (settings.GetValue("BOSS::displayKey"));
        showGUI = Convert.ToBoolean(settings.GetValue("BOSS::showGUI"));
        print("Loaded BOSS settings.");
    }
}