using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : enemy
{
    private Transform weak1;
    private Transform weak2;

    protected override void Awake()
    {
        setCurrentPV(m_MaxPV);
    }

    // Start is called before the first frame update
    void Start()
    {
        weak1 = gameObject.transform.GetChild(0);
        weak2 = gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        isDead();
    }

    override protected void move()
    {
        if (m_mainCamera.WorldToScreenPoint(transform.position).y < m_mainCamera.pixelHeight *0.9)
        {
            transform.position += Vector3.right*Mathf.Cos(Time.deltaTime) ;
        }
        else
        {
            transform.position += Vector3.back * Time.deltaTime;
        }
    }
}
