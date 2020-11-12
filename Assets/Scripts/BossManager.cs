﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private Boss1 boss1;
    [SerializeField]
    private GameObject m_spaceship;
    [SerializeField]
    private Camera m_mainCamera;
    [SerializeField]
    private Player m_player;
    bool isPoped;

    private float height;
    // Start is called before the first frame update
    void Start()
    {
        isPoped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player.getScore() >= 100 && !isPoped)
        {
            Debug.Log(m_player.getScore());
            popBoss1();
        }
    }

    void popBoss1()
    {
        boss1.m_mainCamera = m_mainCamera;
        height = m_mainCamera.WorldToScreenPoint(m_spaceship.transform.position).z;
        Vector3 bossPos = m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(m_mainCamera.pixelWidth * 0.3f, m_mainCamera.pixelWidth * 0.6f), m_mainCamera.pixelHeight, height));
        boss1.transform.position = bossPos;
        Instantiate(boss1);
        isPoped = true;
    }
}
