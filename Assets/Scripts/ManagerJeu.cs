using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerJeu : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Text Score;

    private void Update()
    {
        scoreUpdate();
    }

    public void scoreUpdate()
    {
        Score.text = player.getScore().ToString();
    }
}
