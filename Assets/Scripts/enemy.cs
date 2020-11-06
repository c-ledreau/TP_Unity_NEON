using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : Entity
{
    [SerializeField]
    private float m_enemySpeed;
    [SerializeField]
    public Camera m_mainCamera;
    private TrailRenderer trail;

    protected override void Awake()
    {
        setCurrentPV(m_MaxPV);
    }

    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        isDead();
            trail.SetPosition(trail.positionCount,new Vector3(-10, -10, -10));
    }

    protected void isDead()
    {
        if (getCurrentPV() <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    virtual protected void move()
    {
        transform.position += Vector3.back * Time.deltaTime * m_enemySpeed;
        if (m_mainCamera.WorldToScreenPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log("aaaaa");

        if (collision.gameObject.tag == "Player")
        {
            setCurrentPV(getCurrentPV() - 100);
        }

        if (collision.gameObject.tag == "bullet")
        {
            Bullet bull = collision.gameObject.GetComponent<Bullet>();
            setCurrentPV(getCurrentPV() - bull.getDamage());
            Destroy(bull.gameObject);
        }
    }
}
