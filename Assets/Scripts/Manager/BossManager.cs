using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// handle the behavior of the boss and its protections
/// </summary>
public class BossManager : MonoBehaviour
{
    [SerializeField]
    private Boss1 boss1; //boss prefab
    [SerializeField]
    private GameObject m_spaceship; //player vessel prefab
    [SerializeField]
    private bossProtection m_protection; //protection prefab
    [SerializeField]
    private Camera m_mainCamera;
    [SerializeField]
    private Player m_player;
    [SerializeField]
    private GameObject m_enemyManager;
    [SerializeField]
    private TextMeshProUGUI m_alert;
    bool isPoped;

    [SerializeField]
    private AudioSource m_alarme;

    [SerializeField]
    private float m_scoreForBoss = 100;

    private float height;
    // Start is called before the first frame update
    void Start() //initialization of its feature
    {
        Boss1.Udead += newBoss;
        isPoped = false;
        m_alert.gameObject.SetActive(true);
        m_alert.CrossFadeAlpha(0.0f, 0, false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (m_player.getScore() >= m_scoreForBoss && !isPoped)
        {
            Debug.Log(m_player.getScore());
            StartCoroutine(PopBoss1());
            //StartCoroutine(PopProtection());
            isPoped = true;
        }
    }

    IEnumerator PopBoss1() //spawn the boss and its protections
    {
        //end spawn enemy
        m_enemyManager.SetActive(false);
        m_alert.gameObject.SetActive(true);
        m_alarme.Play();
        for (int k = 0; k < 4; k++) //modify the sound
        {
            m_alert.CrossFadeAlpha(1.0f, 0.5f, false);
            yield return new WaitForSeconds(.86f);
            m_alert.CrossFadeAlpha(0.0f, 0.5f, false);
            yield return new WaitForSeconds(.86f);
        }


        boss1.m_mainCamera = m_mainCamera;
        height = m_mainCamera.WorldToScreenPoint(m_spaceship.transform.position).z;
        Vector3 bossPos = m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(m_mainCamera.pixelWidth * 0.3f, m_mainCamera.pixelWidth * 0.6f), m_mainCamera.pixelHeight+70, height)); //spawn the boos out of the screen randomly
        boss1.transform.position = bossPos;
        Instantiate(boss1);

        m_protection.m_mainCamera = m_mainCamera;
        height = m_mainCamera.WorldToScreenPoint(m_spaceship.transform.position).z;
        for (int j = 0; j < 4; j++)//spawn the first row of protection
        {
            Vector3 proPos = m_mainCamera.ScreenToWorldPoint(new Vector3((j) * m_mainCamera.pixelWidth / 4 , m_mainCamera.pixelHeight * 0.8f, height));
            m_protection.transform.position = proPos;
            m_protection.m_positive = true;
            Instantiate(m_protection);
        }
        for (int j = 0; j < 4; j++)//spawn the second row of protection
        {
            Vector3 proPos = m_mainCamera.ScreenToWorldPoint(new Vector3((j+1 ) * m_mainCamera.pixelWidth / 4 , m_mainCamera.pixelHeight * 0.7f, height));
            m_protection.transform.position = proPos;
            m_protection.m_positive = false;
            Instantiate(m_protection);
        }
    }
    
    private int cnt = 1;
    private void newBoss()//initiolizatioon of the next boss
    {
        m_scoreForBoss = m_player.getScore() + 2500;
        boss1.m_MaxPV += 100;
        cnt++;
        m_alert.text = "Warning\n\nGX2 - RZ STATION MK " + cnt + " incoming";
        isPoped = false;
    }


}
