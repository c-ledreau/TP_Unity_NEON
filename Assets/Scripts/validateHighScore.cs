using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class validateHighScore : MonoBehaviour
{
    private InputField text;
    [SerializeField]
    private Button validate;
    [SerializeField]
    private Text score;
    [SerializeField]
    private TextMeshProUGUI display;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<InputField>();

    }

    // Update is called once per frame
    void Update()
    {
        if (text.text != "")
        {
            validate.interactable = true;
        }
        else
        {
            validate.interactable = false;
        }
    }



    public void validateHS()
    {
        int sco = int.Parse(score.text);
        if(sco >= PlayerPrefs.GetInt("score1"))
        {
            display.text = "Congratulation, you are now the best pilot of the fleet";
            PlayerPrefs.SetInt("score3", PlayerPrefs.GetInt("score2"));
            PlayerPrefs.SetString("name3", PlayerPrefs.GetString("name2"));

            PlayerPrefs.SetInt("score2", PlayerPrefs.GetInt("score1"));
            PlayerPrefs.SetString("name2", PlayerPrefs.GetString("name1"));

            PlayerPrefs.SetInt("score1", sco);
            PlayerPrefs.SetString("name1", text.text);
        }
        else if(sco >= PlayerPrefs.GetInt("score2") && sco < PlayerPrefs.GetInt("score1"))
        {
            display.text = "You are the second best pilot here";
            PlayerPrefs.SetInt("score3", PlayerPrefs.GetInt("score2"));
            PlayerPrefs.SetString("name3", PlayerPrefs.GetString("name2"));

            PlayerPrefs.SetInt("score2", sco);
            PlayerPrefs.SetString("name2", text.text);
        }
        else if(sco >= PlayerPrefs.GetInt("score3") && sco < PlayerPrefs.GetInt("score2"))
        {
            display.text = "With hard work, you could be far better that just the third";
            PlayerPrefs.SetInt("score3", sco);
            PlayerPrefs.SetString("name3", text.text);
        }
        else
        {
            display.text = "You are such a shame, you don't deserve to be remembered";
        }
    }
}
