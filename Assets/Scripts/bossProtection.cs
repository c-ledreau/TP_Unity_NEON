using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossProtection : enemy
{
    private float m_time = 0;
    public bool m_positive;
    [SerializeField]
    private float m_sinIntensity = 20;

    // if true, left trail activated, if false, right trail activated
    private bool m_trail;
    [SerializeField]
    private TrailRenderer m_trailLeft;
    [SerializeField]
    private TrailRenderer m_trailRight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(falseTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override protected void move()
    {
        
        if (m_positive)
        {
            transform.position += new Vector3(Mathf.Sin(m_time) * m_sinIntensity, 0.0f, 0.0f) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(Mathf.Sin(m_time ) * m_sinIntensity * -1, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    IEnumerator falseTime()
    {
        float res = 0;
        while (isActiveAndEnabled)
        {
            res = m_time;
            m_time += Time.deltaTime;
            if (Mathf.Sin(m_time) * m_sinIntensity > 0)// Mathf.Sin(res) * m_sinIntensity)
            {
                m_trailLeft.gameObject.SetActive(true);
                m_trailRight.gameObject.SetActive(false);
            }
            else
            {
                m_trailLeft.gameObject.SetActive(false);
                m_trailRight.gameObject.SetActive(true);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
