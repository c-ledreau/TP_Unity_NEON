using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField]
    private Camera m_MainCamera;
    [SerializeField]
    private float m_VerticalSpeed;
    [SerializeField]
    private float m_HorizontalSpeed;
    Stopwatch stopwatch;
    [SerializeField]
    private Image image;

    //interface membrers
    [SerializeField]
    private Slider HealthBar;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image heathFilling;
    [SerializeField]
    private float score = 0;

    protected override void Awake()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        setCurrentPV(m_MaxPV);

        // gestion heathBar
        HealthBar.maxValue = m_MaxPV;
        UpdateHealthSlider(m_MaxPV);
        heathFilling.color = gradient.Evaluate(1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        image.gameObject.SetActive(false);
        m_speedBullet = bullet.getBulletSpeed();
        m_nbrGun = 1;
        IncreaseDmg();
        Bullet.OnHit += OnBulletHit;
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
    }


    void PlayerControl()
    {
        Vector3 screenPos = m_MainCamera.WorldToScreenPoint(transform.position);
        if (Input.GetKey(KeyCode.LeftArrow) && screenPos.x > 0) 
        {
            transform.position += Vector3.left * Time.deltaTime * m_VerticalSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow) && screenPos.x < m_MainCamera.pixelWidth )
        {
            transform.position += Vector3.right * Time.deltaTime * m_VerticalSpeed;
        }
        if (Input.GetKey(KeyCode.UpArrow) && screenPos.y < m_MainCamera.pixelHeight)
        {
            transform.position += Vector3.forward * Time.deltaTime * m_HorizontalSpeed * 0.5f;
        }
        if (Input.GetKey(KeyCode.DownArrow) && screenPos.y > m_MainCamera.pixelHeight*0.2)
        {
            transform.position += Vector3.back * Time.deltaTime * m_HorizontalSpeed * 2.0f;
        }
        if (Input.GetKey(KeyCode.Space) && stopwatch.Elapsed.TotalMilliseconds >= 1000/m_fireRate)
        {
            bullet.m_MainCamera = m_MainCamera;
            UnityEngine.Debug.Log("aaaaa");
            for (int k = 0; k < m_nbrGun; k++)
            {
                Bullet bull;
                bull = Instantiate(bullet);
                bull.m_direction = 1.0f;
                bull.setDamage(m_dmg);
                bull.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+ 1);
                bull.m_angle = (-(m_nbrGun-1)+k*2)/10.0f;
                bull.transform.localScale = new Vector3(m_bulletScale, m_bulletScale, m_bulletScale);
                bull.setBulletSpeed(m_speedBullet);
            }
            stopwatch.Restart();
        }

    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            setCurrentPV(getCurrentPV() - 50);
            UpdateHealthSlider(getCurrentPV());
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
            UpdateHealthSlider(getCurrentPV());
            Destroy(collision.gameObject);
        }
    }

    private void UpdateHealthSlider(int health)
    {
        HealthBar.value = health;
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
}
