using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float m_powerUpSpeed;
    public Camera m_mainCamera;
    public Player m_spaceShip;
    public bool isSpaceShip;

    void start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        transform.position += Vector3.back * Time.deltaTime * m_powerUpSpeed;
        if (m_mainCamera.WorldToScreenPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            powerUp();
        }
    }

    void powerUp()
    {
        int i = Random.Range(1, 4);
        //int i = 4;
        if (i == 0) //bigger bullets
        {
            Debug.Log("bigger bullets");
            //m_spaceShip.m_bulletScale += 0.2f;
        }
        if (i == 1) //increase fire rate
        {
            Debug.Log("increase fire rate");
            m_spaceShip.IncreaseFireRate();
        }
        if (i == 2) // increase speed bullet
        {
            Debug.Log("increase speed bullet");
            m_spaceShip.IncreaseSpeedBullet();
        }
        if (i == 3) // add new gun
        {
            Debug.Log("add new gun");
            m_spaceShip.IncreaseNbrGun();
        }
        if (i == 4) // increase dps
        {
            Debug.Log("increase dps");
            m_spaceShip.IncreaseDmg();

        }
    }
}
