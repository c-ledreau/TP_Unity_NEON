﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the power up behaviour
/// </summary>
public class PowerUpMananger : MonoBehaviour
{
    [SerializeField]
    private PowerUp m_powerUp; //prefab of the power up
    [SerializeField]
    private float m_spawningTime;
    [SerializeField]
    private Player m_spaceship;
    [SerializeField]
    private Camera m_mainCamera;

    private float height;

    
    private int damageCnt = 0;
    private int fireRateCnt = 0;
    private int bulletSpeedCnt = 0;
    private int nbGunsCnt = 0;


    [SerializeField]
    private GameObject[] fireRateArray;
    [SerializeField]
    private GameObject[] bulletSpeedArray;
    [SerializeField]
    private GameObject[] gunArray;
    [SerializeField]
    private GameObject[] damageArray;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Test());
        //Debug.Log("test");
        height = m_mainCamera.WorldToScreenPoint(m_spaceship.transform.position).z;
        PowerUp.getPow += PowerUpLimitor;
        enemy.spawn += spawPow;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Test()
    {
        while (Application.isPlaying)
        {
            Vector3 foePos = m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(m_mainCamera.pixelWidth * 0.1f, m_mainCamera.pixelWidth * 0.9f), m_mainCamera.pixelHeight, height));
            spawPow(foePos);
            yield return new WaitForSeconds(m_spawningTime);
        }
    }


    /// <summary>
    /// limit the power Ups
    /// </summary>
    /// <param name="pow"> pow is a powTypes enum which can be : fireRate, bulletSpeed, addGun, damage or score</param>
    private void PowerUpLimitor(PowerUp.powTypes pow)
    {
        Debug.Log("limitor");
        m_spaceship.addScore(200);
        switch (pow)
        {
            case PowerUp.powTypes.fireRate:
                {
                    if (fireRateCnt<5)
                    {
                        m_spaceship.IncreaseFireRate();
                        fireRateCnt++;
                        updateImages(fireRateArray, fireRateCnt);
                    }
                    break;
                }
            case PowerUp.powTypes.bulletSpeed:
                {
                    if (bulletSpeedCnt < 5)
                    {
                        m_spaceship.IncreaseSpeedBullet();
                        bulletSpeedCnt++;
                        updateImages(bulletSpeedArray, bulletSpeedCnt);
                    }
                    break;
                }
            case PowerUp.powTypes.addGun:
                {
                    if (nbGunsCnt < 5)
                    {
                        m_spaceship.IncreaseNbrGun();
                        nbGunsCnt++;
                        updateImages(gunArray, nbGunsCnt);
                    }
                    break;
                }
            case PowerUp.powTypes.damage:
                {
                    if (damageCnt < 5)
                    {
                        m_spaceship.IncreaseDmg();
                        damageCnt++;
                        updateImages(damageArray, damageCnt);
                    }
                    break;
                }
                case PowerUp.powTypes.heal:
                {
                    if (damageCnt < 5)
                    {
                        m_spaceship.Heal();
                        m_spaceship.UpdateHealthSlider();
                    }
                    break;
                }
        }
    }

    /// <summary>
    /// update the UI at the bottom of the screen
    /// </summary>
    /// <param name="ImageArray"> the image from the scene</param>
    /// <param name="lvl"></param>
    private void updateImages(GameObject[] ImageArray, int lvl)
    {
        for (int i = 0; i < ImageArray.Length; i++)
        {
            if (lvl > i)
            {
                ImageArray[i].gameObject.SetActive(true);
            }
            else
            {
                ImageArray[i].gameObject.SetActive(false);

            }
        }
    }

    /// <summary>
    /// instanntiate a power up
    /// </summary>
    /// <param name="pos"> instatiation of the power at the pos position</param>
    public void spawPow(Vector3 pos)
    {
        m_powerUp.m_mainCamera = m_mainCamera;
        m_powerUp.m_spaceShip = m_spaceship;
        m_powerUp.transform.position = pos;
        Instantiate(m_powerUp);
    }

}
