using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// this class describe the behavior of the enemies that shoot
/// </summary>
public class shootingEnemy : enemy
{
    protected Stopwatch stopwatch;
    // Start is called before the first frame update
    void Start() //initialization of its features
    {
        m_speedBullet = bullet.getBulletSpeed();
        m_nbrGun = 1;
        m_dmg = 5;
        explosion = transform.GetComponent<ParticleSystem>();
    }

    protected override void Awake()//initialization of its features
    {
        setCurrentPV(m_MaxPV);
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }
    // Update is called once per frame
    void Update()
    {
        isDead();
        shootingControl();
    }

    /// <summary>
    /// handles the shooting behavior of the shooting enemy
    /// </summary>
    protected void shootingControl()
    {
        if (stopwatch.Elapsed.TotalMilliseconds*Time.timeScale >= 1000 / m_fireRate) 
        {
            bullet.m_MainCamera = m_mainCamera;
            for (int k = 0; k < m_nbrGun; k++)//define the characteritics of the shots
            {
                Bullet bull;
                bull = Instantiate(bullet);
                bull.m_direction = -1.0f;
                bull.setDamage(m_dmg);
                bull.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                bull.m_angle = (-(m_nbrGun - 1) + k * 2) / 10.0f;
                bull.transform.localScale = new Vector3(m_bulletScale, m_bulletScale, m_bulletScale);
                bull.setBulletSpeed(m_speedBullet);
            }
            stopwatch.Restart();
        }
    }
}
