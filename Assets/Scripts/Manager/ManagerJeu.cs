using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// score manager
/// </summary>
public class ManagerJeu : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Text Score;
    [SerializeField]
    private Text Score2;

    private void Update()
    {
        scoreUpdate();
    }

    public void scoreUpdate()
    {
        Score.text = player.getScore().ToString();
        Score2.text = player.getScore().ToString();
    }
}
