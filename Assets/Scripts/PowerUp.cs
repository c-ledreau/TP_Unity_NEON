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

    public delegate void PowerTake(powTypes pow);
    public static event PowerTake getPow;

    public enum powTypes
    {
        fireRate,
        bulletSpeed,
        addGun,
        damage,
        score,
        heal,
    };

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
        powTypes pow = powTypes.score;
        int i = Random.Range(0, 5);
        //int i = 4;
        if (i == 0) //bigger bullets
        {
            Debug.Log("Heal");
            //m_spaceShip.m_bulletScale += 0.2f;
            pow = powTypes.heal;
        }
        if (i == 1) //increase fire rate
        {
            Debug.Log("increase fire rate");
            //m_spaceShip.IncreaseFireRate();
            pow = powTypes.fireRate;
        }
        if (i == 2) // increase speed bullet
        {
            Debug.Log("increase speed bullet");
            //m_spaceShip.IncreaseSpeedBullet();
            pow = powTypes.bulletSpeed;
        }
        if (i == 3) // add new gun
        {
            Debug.Log("add new gun");
            //m_spaceShip.IncreaseNbrGun();
            pow = powTypes.addGun;
        }
        if (i == 4) // increase dps
        {
            Debug.Log("increase dps");
            //m_spaceShip.IncreaseDmg();
            pow = powTypes.damage;

        }
        getPow(pow);
    }
}
