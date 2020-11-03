using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : EditorWindow
{
    List<SceneData> listScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [MenuItem("Tools/Scenes manager")]
    static void ShowWindow()
    {
        GetWindow<ScenesManager>("new window");
    }
    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("hello world");
        if (GUILayout.Button("click"))
        {
            Debug.Log("the cake IS A LIE");
        }
        GUILayout.EndVertical();
    }
    void refreshContent()
    {
        listScene.Clear();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            listScene.Add(new SceneData(Path.GetFileNameWithoutExtension(scene.path), scene.path, false, false));
        }
        for (int k = 0; k < EditorSceneManager.sceneCount; k++)
        {
            //EditorSceneManager.GetSceneAt(k).buildIndex;
        }
    }
}

public class SceneData
{
    public string m_name;
    public string m_path;
    public bool m_isActive;
    public bool m_isActiveNext;

    public SceneData(string p_name, string p_path, bool p_isActive, bool p_isActiveNext)
    {
        m_name = p_name;
        m_path = p_path;
        m_isActive = p_isActive;
        m_isActiveNext = p_isActiveNext;
    }
}
