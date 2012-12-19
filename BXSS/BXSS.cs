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

using System.Globalization;
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


    public BXSSSettings settings;

    public int screenshotCount;
    public string superSampleValueString = "1";

    public bool display = false;
    public bool showHelp = false;
    public int i;
    public bool showGUI = false;

    public void Awake()
    {
        settings = new BXSSSettings();
        settings.Load();
        DontDestroyOnLoad(this);
    }

    private void WindowGUI(int windowID)
    {
        GUIStyle mainGUI = new GUIStyle(GUI.skin.button);

    //    mainGUI.padding = new RectOffset(8, 8, 8, 8);

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
            GUILayout.Label("Current supersample value: " + settings.SupersampleAmount, GUILayout.ExpandHeight(true),
                            GUILayout.ExpandWidth(true));
            GUILayout.Label("Supersample value: ");
            superSampleValueString = GUILayout.TextField(superSampleValueString);

            int newSuperSampleValue;
            if(int.TryParse(superSampleValueString, out newSuperSampleValue) && newSuperSampleValue >= 1)
            {
                settings.SupersampleAmount = newSuperSampleValue;
                superSampleValueString = settings.SupersampleAmount.ToString(CultureInfo.InvariantCulture);
            }

            GUILayout.Label("You have taken " + screenshotCount + " screenshots.");

            GUILayout.EndVertical();
        }
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    public void screenshotMethod()
    {
        string screenshotFilename =  "Screenshot" + screenshotCount;
        Application.CaptureScreenshot(KSPUtil.ApplicationRootPath + settings.ScreenshotDirectory + screenshotFilename + ".png", settings.SupersampleAmount);
        screenshotCount++;
        settings.Save();
      //  saveSettings();
    }

    private void OnGUI()
    {
       // if (FlightGlobals.fetch != null && FlightGlobals.ActiveVessel != null
        if(display)
        {
            GUI.skin = HighLogic.Skin;
            settings.WindowPosition = GUILayout.Window(569, settings.WindowPosition, WindowGUI, "B.X.S.S.",
                                         showHelp
                                             ? new[] {GUILayout.Width(180), GUILayout.ExpandHeight(true)}
                                             : new[] {GUILayout.Width(120), GUILayout.Height(60)});
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(settings.ScreenshotKey))
            screenshotMethod();

        if (Input.GetKeyDown(settings.DisplayKey))
            display = !display;
    }
}