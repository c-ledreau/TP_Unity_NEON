using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class displayHighscore : MonoBehaviour
{
    public List<TextMeshProUGUI> listWrite;
    public GameObject uiMain;
    public GameObject uiHS;
    int k,i;

    // Start is called before the first frame update
    void Start()
    {
        displayScore();
    }
    public void resetScore()
    {
        PlayerPrefs.DeleteAll();
        displayScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void toHS()
    {
        uiMain.SetActive(false);
        uiHS.SetActive(true);
    }
    public void toMain()
    {
        uiMain.SetActive(true);
        uiHS.SetActive(false);
    }

    private void displayScore()
    {
        k = 0;
        i = 0;
        if (PlayerPrefs.HasKey("name1"))
        {
            Debug.Log(PlayerPrefs.GetString("name1"));
            Debug.Log(PlayerPrefs.GetInt("score1"));
            listWrite[0].text = PlayerPrefs.GetString("name1");
            listWrite[1].text = PlayerPrefs.GetInt("score1").ToString();
        }
        if (PlayerPrefs.HasKey("name2"))
        {
            Debug.Log(PlayerPrefs.GetString("name2"));
            Debug.Log(PlayerPrefs.GetInt("score2"));
            listWrite[2].text = PlayerPrefs.GetString("name2");
            listWrite[3].text = PlayerPrefs.GetInt("score2").ToString();
        }
        if (PlayerPrefs.HasKey("name3"))
        {
            Debug.Log(PlayerPrefs.GetString("name3"));
            Debug.Log(PlayerPrefs.GetInt("score3"));
            listWrite[4].text = PlayerPrefs.GetString("name3");
            listWrite[5].text = PlayerPrefs.GetInt("score3").ToString();
        }
    }
}
