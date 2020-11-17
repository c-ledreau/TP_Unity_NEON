using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private Boss1 boss1; //boss prefab
    [SerializeField]
    private GameObject m_spaceship; //player vessel prefab
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
            isPoped = true;
        }
    }

    IEnumerator PopBoss1()
    {
        //end spawn enemy
        m_enemyManager.SetActive(false);
        m_alert.gameObject.SetActive(true);
        m_alarme.Play();
        for (int k = 0; k < 4; k++)
        {
            m_alert.CrossFadeAlpha(1.0f, 0.5f, false);
            yield return new WaitForSeconds(.86f);
            m_alert.CrossFadeAlpha(0.0f, 0.5f, false);
            yield return new WaitForSeconds(.86f);
        }

        //yield return new WaitForSeconds(.5f);
        //text appears
            //spawn boss1


        boss1.m_mainCamera = m_mainCamera;
        height = m_mainCamera.WorldToScreenPoint(m_spaceship.transform.position).z;
        Vector3 bossPos = m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(m_mainCamera.pixelWidth * 0.3f, m_mainCamera.pixelWidth * 0.6f), m_mainCamera.pixelHeight+70, height));
        boss1.transform.position = bossPos;
        Instantiate(boss1);
    }

    private void newBoss()
    {
        m_scoreForBoss = m_player.getScore() + 3000;
        isPoped = false;
    }


}
