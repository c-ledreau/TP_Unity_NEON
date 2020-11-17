using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

/// <summary>
/// descriibes the behaviour of the boss
/// </summary>
public class Player : Entity
{
    [SerializeField]
    private Camera m_MainCamera; //set the camera of the player 
    public float m_VerticalSpeed; //set the vertical speed of the player 
    public float m_HorizontalSpeed; //set the horizontal speed of the player 
    Stopwatch stopwatch;
    [SerializeField]
    private Image image; 
    private TrailRenderer line;

    //interface membrers
    [SerializeField]
    private Slider HealthBar;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image heathFilling;
    [SerializeField]
    private float score = 0;

    public AudioSource AudioSource;

    public Bullet.patterns pattern = Bullet.patterns.Base;

    float vertical;
    private Rigidbody rb;

    protected override void Awake()//initialization of its features
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        setCurrentPV(m_MaxPV);

        // gestion heathBar
        HealthBar.maxValue = m_MaxPV;
        UpdateHealthSlider();
        heathFilling.color = gradient.Evaluate(1f);
    }

    // Start is called before the first frame update
    void Start()//initialization of its features
    {
        image.gameObject.SetActive(false);
        m_speedBullet = bullet.getBulletSpeed();
        m_nbrGun = 1;
        IncreaseDmg();
        enemy.OnDestruct += addScore;
        line = transform.GetComponent<TrailRenderer>();
        rb = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (getCurrentPV() <= 0) // if the player has no more HP, destroys it and set the pause on
        {
            Destroy(gameObject);
            image.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerControl();
        Trail();
    }

    /// <summary>
    /// handle the movement of the trail
    /// </summary>
    void Trail()
    {
        for (int k = 1; k < line.positionCount; k++)
        {
            line.SetPosition(k, line.GetPosition(k) + Vector3.back * 0.3f); // set an offset of the trail to make look like the player is moving a lot through space (it is actuallu almost motionless)
        }
    }

    /// <summary>
    /// movement of the player
    /// </summary>
    void PlayerControl()
    {
        Vector3 screenPos = m_MainCamera.WorldToScreenPoint(transform.position);
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(mH * m_HorizontalSpeed, 0, mV * m_VerticalSpeed + 0.5f); //set the velocity of the player, regarding the keys pressed

        //start : force the player to stay in front of the camera
        if (screenPos.x < 0)
        {
            rb.velocity = new Vector3(1, 0, 0);
        }
        if (screenPos.x > m_MainCamera.pixelWidth)
        {
            rb.velocity = new Vector3(-1, 0, 0);
        }
        if (screenPos.y > m_MainCamera.pixelHeight)
        {
            rb.velocity = new Vector3(0, 0, -1);
        }
        if (screenPos.y < m_MainCamera.pixelHeight * 0.2)
        {
            rb.velocity = new Vector3(0, 0, 1);
        }
        //end : force the player to stay in front of the camera

        if (Input.GetKey(KeyCode.Space) && stopwatch.Elapsed.TotalMilliseconds >= 1000 / m_fireRate)//define the characteritics of the shots
        {
            bullet.m_MainCamera = m_MainCamera;
            for (int k = 0; k < m_nbrGun; k++)
            {
                AudioSource.Play();
                Bullet bull;
                bull = Instantiate(bullet);
                bull.m_direction = 1.0f;
                bull.setDamage(m_dmg);
                if (pattern == Bullet.patterns.Sinus)
                    bull.transform.position = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z + 3);
                else
                    bull.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3);
                bull.m_angle = (-(m_nbrGun - 1) + k * 2) / 10.0f;
                bull.transform.localScale = new Vector3(m_bulletScale, m_bulletScale, m_bulletScale);
                bull.setBulletSpeed(m_speedBullet);
                bull.setOrigine(true);
                bull.pattern = pattern;

            }
            stopwatch.Restart();
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "enemy") //looses 20 HP if the player collides with an enemy
        {
            setCurrentPV(getCurrentPV() - 20);
            UpdateHealthSlider();
        }

        if (collision.gameObject.tag == "PowerUp")//destroy the power if it collides with the player
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "bullet") //the player looses the damage of the bullet the the bullet is destroyed
        {
            Bullet rec = collision.gameObject.GetComponent<Bullet>();
            setCurrentPV(getCurrentPV() - rec.getDamage());
            Destroy(collision.gameObject);
            UpdateHealthSlider();
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// update the slider of the healthbar
    /// </summary>
    public void UpdateHealthSlider()
    {
        HealthBar.value = getCurrentPV();
        heathFilling.color = gradient.Evaluate(HealthBar.normalizedValue);
    }

    /// <summary>
    /// increase the score of the player by addScore
    /// </summary>
    /// <param name="addScore"></param>
    private void OnBulletHit(float addScore)
    {
        score += addScore;
    }

    public float getScore()
    {
        return score;
    }

    /// <summary>
    /// increase the score of the player by add
    /// </summary>
    /// <param name="add"></param>

    public void addScore(float add)
    {
        score += add;
    }
}
