
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : EditorWindow
{
    List<SceneData> listScene = new List<SceneData>();
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
        if (GUILayout.Button("Refresh the list"))
        {
            refreshContent();
        }

        GUILayout.BeginScrollView(Vector2.zero);
        foreach (SceneData data in listScene)
        {
            data.m_isActiveNext = GUILayout.Toggle(data.m_isActiveNext, data.m_name);

            if (data.m_isActiveNext)
            {
                EditorSceneManager.OpenScene(data.m_path, OpenSceneMode.Additive);
            }
            else
            {
                if (EditorSceneManager.sceneCount > 0)
                {
                    EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByName(data.m_name), true);
                }
                else
                {
                    Debug.Log("You must have at least one loaded scene");
                    GUILayout.Label("You must have at least one loaded scene");
                    data.m_isActiveNext = true;
                }
            }
            data.m_isActive = data.m_isActiveNext;
        }

        GUILayout.EndScrollView();

        GUILayout.EndVertical();
    }

    [MenuItem("Assets/Scene/Add to build", false)]
    static  void AddToBuild()
    {
        if (AddToBuildValidate())
        {
            SceneAsset toBuild = Selection.activeObject as SceneAsset;
            List<EditorBuildSettingsScene> listBuild = new List<EditorBuildSettingsScene>();
            List<string> listBuildName = new List<string>();

            foreach (EditorBuildSettingsScene data in EditorBuildSettings.scenes)
            {
                listBuild.Add(data);
                listBuildName.Add(Path.GetFileNameWithoutExtension(data.path));
            }
            if (listBuildName.Contains(toBuild.name))
            {
                Debug.Log("cette scène est déjà dans la liste des scènes du build");
            }
            else
            {
                Debug.Log(toBuild.name + " est maintenant dans la liste des scène à build");
                listBuild.Add(new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(toBuild), true));
                EditorBuildSettings.scenes = listBuild.ToArray();
            }
        }
    }

    [MenuItem("Assets/Scene/Add to build", true)]
    static bool AddToBuildValidate()
    {
        var test = Selection.activeObject;
        if (test.GetType() == typeof(SceneAsset))
        {
            return true;
        }
        return false;
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
            listScene[EditorSceneManager.GetSceneAt(k).buildIndex+1].m_isActive = true;
            listScene[EditorSceneManager.GetSceneAt(k).buildIndex+1].m_isActiveNext = true; 
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
