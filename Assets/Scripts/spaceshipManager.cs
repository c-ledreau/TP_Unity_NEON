using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceshipManager : MonoBehaviour
{
    public GameObject[] starchasers;
    public GameObject player;
    private int cpt;
    public GameObject eneMan;
    public GameObject bossMan;
    public GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        cpt = 0;
        //eneMan.SetActive(false);
        bossMan.SetActive(false);
        player.SetActive(false);
        DisplayStarchaser();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goLeft()
    {
        cpt--;
        if (cpt <= -1)
            cpt = 2;
        DisplayStarchaser();
    }

    public void goRight()
    {
        cpt++;
        if (cpt >= 3)
            cpt = 0;
        DisplayStarchaser();
    }

    public void DisplayStarchaser()
    {
        foreach (GameObject star in starchasers)
        {
            star.SetActive(false);
        }
        starchasers[cpt].SetActive(true);
    }

    public void select()
    {
        eneMan.SetActive(true);
        bossMan.SetActive(true);
        player.SetActive(true);
        UI.SetActive(false);
        modifyPlayer();
        player.transform.GetChild(cpt).gameObject.SetActive(true);
    }

    private void modifyPlayer()
    {
        Player ply = player.GetComponent<Player>();
        if (cpt == 1)
        {
            ply.m_VerticalSpeed = 5;
            ply.m_HorizontalSpeed = 5;
            ply.m_MaxPV = 120;
            ply.m_fireRate = 3;
            ply.IncreaseDmg();
        }
        if (cpt == 2)
        {
            ply.m_MaxPV = 80;
            ply.m_fireRate = 6;
            player.GetComponent<BoxCollider>().size -= new Vector3(0,0.008f,0);
        }
    }
}
