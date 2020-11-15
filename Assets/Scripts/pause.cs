using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField]
    private GameObject UIPause;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
