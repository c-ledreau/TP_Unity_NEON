using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// load the given scene
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public void loadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
