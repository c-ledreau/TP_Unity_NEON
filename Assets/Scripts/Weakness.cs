 using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// describes the behaviour of the weaknesses of the boss
/// </summary>

public class Weakness : shootingEnemy
{

    private Boss1 boss1; 
    protected override void Awake() //initialization of its features
    {
        setCurrentPV(m_MaxPV);
        stopwatch = new Stopwatch();
        stopwatch.Start();
        m_dmg = 5;
    }
    // Start is called before the first frame update
    void Start()//initialization of its features
    {
        boss1 = transform.parent.gameObject.GetComponent<Boss1>();
        m_mainCamera = boss1.m_mainCamera;
        m_speedBullet = bullet.getBulletSpeed();
        explosion = transform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        isDead();
        shootingControl();
    }

    void OnCollisionEnter(Collision collision) //when it collides with a bullet, looses HP and make loose incerased HP to the boss 
    {
        if (collision.gameObject.tag == "bullet")
        {
            Bullet bull = collision.gameObject.GetComponent<Bullet>();
            boss1.setCurrentPV(boss1.getCurrentPV() - (int)(bull.getDamage()*1.5f));
            setCurrentPV(getCurrentPV() - (int)(bull.getDamage() * 1.5f));
        }

    }
}
