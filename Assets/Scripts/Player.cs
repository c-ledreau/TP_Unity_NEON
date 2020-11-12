using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField]
    private Camera m_MainCamera;
    public float m_VerticalSpeed;
    public float m_HorizontalSpeed;
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

    public Bullet.patterns pattern = Bullet.patterns.Base;

    float vertical;
    private Rigidbody rb;

    protected override void Awake()
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
    void Start()
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
        if (getCurrentPV() <= 0)
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

    void Trail()
    {
        for (int k = 1; k < line.positionCount; k++)
        {
            line.SetPosition(k, line.GetPosition(k) + Vector3.back * 0.3f);
        }
    }

    void PlayerControl()
    {
        Vector3 screenPos = m_MainCamera.WorldToScreenPoint(transform.position);
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(mH * m_HorizontalSpeed, 0, mV * m_VerticalSpeed + 0.5f);

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
        if (Input.GetKey(KeyCode.Space) && stopwatch.Elapsed.TotalMilliseconds >= 1000 / m_fireRate)
        {
            bullet.m_MainCamera = m_MainCamera;
            for (int k = 0; k < m_nbrGun; k++)
            {
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

        if (collision.gameObject.tag == "enemy")
        {
            setCurrentPV(getCurrentPV() - 20);
            UpdateHealthSlider();
        }

        if (collision.gameObject.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "bullet")
        {
            Bullet rec = collision.gameObject.GetComponent<Bullet>();
            setCurrentPV(getCurrentPV() - rec.getDamage());
            Destroy(collision.gameObject);
            UpdateHealthSlider();
            Destroy(collision.gameObject);
        }
    }

    public void UpdateHealthSlider()
    {
        HealthBar.value = getCurrentPV();
        heathFilling.color = gradient.Evaluate(HealthBar.normalizedValue);
    }

    private void OnBulletHit(float addScore)
    {
        score += addScore;
    }

    public float getScore()
    {
        return score;
    }

    public void addScore(float add)
    {
        score += add;
    }
}
