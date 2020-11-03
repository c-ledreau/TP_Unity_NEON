using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Weakness : shootingEnemy
{
    private Boss1 boss1;
    protected override void Awake()
    {
        setCurrentPV(m_MaxPV);
        stopwatch = new Stopwatch();
        stopwatch.Start();
        m_dmg = 5;
    }
    // Start is called before the first frame update
    void Start()
    {
        boss1 = transform.parent.gameObject.GetComponent<Boss1>();
        m_mainCamera = boss1.m_mainCamera;
        m_speedBullet = bullet.getBulletSpeed();
        m_nbrGun = 2;
    }

    // Update is called once per frame
    void Update()
    {
        isDead();
        shootingControl();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Bullet bull = collision.gameObject.GetComponent<Bullet>();
            boss1.setCurrentPV(boss1.getCurrentPV() - (int)(bull.getDamage()*1.5f));
            setCurrentPV(getCurrentPV() - (int)(bull.getDamage() * 1.5f));
        }
    }
}
