using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manage the selection of the of the spaceship
/// </summary>
public class spaceshipManager : MonoBehaviour
{
    public GameObject[] starchasers; //list of the different spaceships
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
        bossMan.SetActive(false); //deactivation of the boss manager
        player.SetActive(false); //deactivation of the enemy manager
        DisplayStarchaser();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// display the spaceship on the left
    /// </summary>
    public void goLeft()
    {
        cpt--;
        if (cpt <= -1)
            cpt = 2;
        DisplayStarchaser();
    }

    /// <summary>
    /// display the spaceship on the right
    /// </summary>
    public void goRight()
    {
        cpt++;
        if (cpt >= 3)
            cpt = 0;
        DisplayStarchaser();
    }

    /// <summary>
    /// display the spaceship regarding cpt
    /// </summary>
    public void DisplayStarchaser()
    {
        foreach (GameObject star in starchasers)
        {
            star.SetActive(false);
        }
        starchasers[cpt].SetActive(true);
    }

    /// <summary>
    /// vaidation of the spaceship + activation of the managers
    /// </summary>
    public void select()
    {
        eneMan.SetActive(true);
        bossMan.SetActive(true);
        player.SetActive(true);
        UI.SetActive(false);
        modifyPlayer();
        player.transform.GetChild(cpt).gameObject.SetActive(true);
    }

    /// <summary>
    /// modify the spaceship regarding which one was chosen
    /// </summary>
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
