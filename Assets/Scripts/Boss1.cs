﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : enemy
{
    private Transform weak1;
    private Transform weak2;
    [SerializeField]
    private GameObject las;
    private LineRenderer m_line;
    private ParticleSystem m_load;

    int dir;

    protected override void Awake()
    {
        setCurrentPV(m_MaxPV);
    }

    // Start is called before the first frame update
    void Start()
    {
        weak1 = gameObject.transform.GetChild(0);
        weak2 = gameObject.transform.GetChild(1);
        dir = 1;
        explosion = transform.GetComponent<ParticleSystem>();
        m_line = las.transform.GetComponent<LineRenderer>();
        m_load = las.transform.GetComponent<ParticleSystem>();
        //scoreOnDestruct = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if (getCurrentPV() <= m_MaxPV )
        {
            StartCoroutine(Death());
        }
        //isDead();
    }

    IEnumerator Death()
    {
        m_load.Play();
        yield return new WaitForSeconds(3);
        m_line.SetPosition(0, las.transform.position);
        m_line.SetPosition(1, las.transform.position + new Vector3(0, 0, -100));
    }

    override protected void move()
    {

        if (m_mainCamera.WorldToScreenPoint(transform.position).y < m_mainCamera.pixelHeight *0.9)
        {
            if(m_mainCamera.WorldToScreenPoint(transform.position).x < m_mainCamera.pixelWidth * 0.3)
            {
                dir = 1;
            }
            if (m_mainCamera.WorldToScreenPoint(transform.position).x > m_mainCamera.pixelWidth * 0.3 + m_mainCamera.pixelWidth * 0.3)
            {
                dir = -1;
            }
            transform.position += Vector3.right * Time.deltaTime * 5 * dir;
        }
        else
        {
            transform.position += Vector3.back * Time.deltaTime*4;
        }
    }
}
