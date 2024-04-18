using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneManagerX : EditorWindow
{

    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    public List<ScenesDetails> scenelist;
    public Vector2 scrollPosition;
    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Scene Manager")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SceneManagerX window = (SceneManagerX)EditorWindow.GetWindow(typeof(SceneManagerX));
        window.Show();

    }

    private const string activeSign = " ✔";
    void OnGUI()
    {
        scenelist = ReadDetails();

        Scene activeScene = EditorSceneManager.GetActiveScene();
        GUILayout.Label(Application.productName, EditorStyles.boldLabel);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(150), GUILayout.Height(150));
        foreach (ScenesDetails item in scenelist)
        {
            string name = item.Name;
            name += item.Path.Equals(activeScene.path) ? activeSign : "";

            //Debug.Log (item.Name);
            if (GUILayout.Button(name))
            {
                Debug.Log("Open Scene" + item.Name);

                if (!EditorApplication.isPlaying)
                {
                    EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
                    EditorSceneManager.OpenScene(item.Path);
                }

            }
        }

        GUILayout.EndScrollView();
    }
    private static List<string> ReadNames()
    {
        List<string> temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);
                temp.Add(name);
                //Debug.Log (name);
            }
        }
        return temp;//.ToArray();
    }
    private static List<ScenesDetails> ReadDetails()
    {
        List<ScenesDetails> temp = new List<ScenesDetails>();

        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);
                ScenesDetails s = new ScenesDetails();
                s.Name = name;
                s.Path = S.path.ToString();
                temp.Add(s);
                //Debug.Log (name);
            }
        }
        return temp;//.ToArray();
    }

}
public class ScenesDetails
{
    public string Name;
    public string Path;
}