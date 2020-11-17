using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the pause eventn accessible by pressing escape or by clicking the pause button
/// </summary>
public class pause : MonoBehaviour
{
    private bool isPaused = false; //the boolean that switches the pause UI
    [SerializeField]
    private GameObject UIPause; //the UI to activate
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    /// <summary>
    /// pressing escape in the update function switch the state of isPaused
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            isPaused = false;
        }

        performPause();
    }

    /// <summary>
    /// this function is called by clicking the pause button
    /// </summary>
    public void setPause()
    {
        if (!isPaused)
        {
            isPaused = true;
        }
        else if (isPaused)
        {
            isPaused = false;
        }
    }

    /// <summary>
    /// pause the game regarding the state of isPaused
    /// </summary>
    public void performPause()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            UIPause.SetActive(true);
        }
        else if (!isPaused)
        {
            Time.timeScale = 1;
            UIPause.SetActive(false);
        }
    }
}
