using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class describe the boss protections behaviour 
/// </summary>
public class bossProtection : enemy
{
    private float m_time = 0; //local time used in the coroutine
    public bool m_positive; // define if the protection goes first on the right or the left
    [SerializeField]
    private float m_sinIntensity = 20; // sinusoidal movment intensity
    private bool m_trail; // if true, left trail activated, if false, right trail activated

    // trailRenderers of the rockets
    [SerializeField]
    private TrailRenderer m_trailLeft; 
    [SerializeField]
    private TrailRenderer m_trailRight;

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetComponent<ParticleSystem>();
        StartCoroutine(falseTime());
        StartCoroutine(Wai());
        //Boss1.Udead += depop;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// sinusoidal movment on the x axis, if m_positive it goes first on the right else on the left
    /// </summary>
    override protected void move()
    {
        if (m_positive)
        {
            transform.position += new Vector3(Mathf.Sin(m_time) * m_sinIntensity, 0.0f, 0.0f) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(Mathf.Sin(m_time) * m_sinIntensity * -1, 0.0f, 0.0f) * Time.deltaTime;
        }

    }


    /// <summary>
    /// alow the protections to use the same time an activate the trail on the left or right rocket
    /// </summary>
    /// <returns></returns>
    IEnumerator falseTime()
    {
        float res = 0;
        while (isActiveAndEnabled)
        {
            res = m_time;
            m_time += Time.deltaTime;
            if (!m_positive)
            {
                if (Mathf.Sin(m_time) * m_sinIntensity > 0)
                {
                    m_trailLeft.gameObject.SetActive(true);
                    m_trailRight.gameObject.SetActive(false);
                }
                else
                {
                    m_trailLeft.gameObject.SetActive(false);
                    m_trailRight.gameObject.SetActive(true);
                }
            }
            else
            {
                if (Mathf.Sin(m_time) * m_sinIntensity > 0)
                {
                    m_trailLeft.gameObject.SetActive(false);
                    m_trailRight.gameObject.SetActive(true);
                }
                else
                {
                    m_trailLeft.gameObject.SetActive(true);
                    m_trailRight.gameObject.SetActive(false);
                }

            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Wai()
    {
        yield return new WaitForSeconds(20);
        setCurrentPV(0);
        isDead();
    }
    /*
    private void depop()
    {
        if(isActiveAndEnabled)
            Destroy(gameObject);
    }*/
}
