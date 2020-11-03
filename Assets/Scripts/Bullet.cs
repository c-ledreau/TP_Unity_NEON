using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_bulletSpeed;
    [SerializeField]
    public Camera m_MainCamera;
    public float m_angle;
    [SerializeField]
    private int m_damage = 5;
    public float m_direction;



    public delegate void OnHitAction(float scoreHit);
    public static event OnHitAction OnHit;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    public void setBulletSpeed(float nbr)
    {
        m_bulletSpeed = nbr;
    }

    public float getBulletSpeed()
    {
        return m_bulletSpeed;
    }

    public void setDamage(int dmg)
    {
        m_damage = dmg;
    }

    public int getDamage()
    {
        return m_damage;
    }

    private void move()
    {
        transform.position += new Vector3(m_angle,0.0f,m_direction) * Time.deltaTime * m_bulletSpeed;
        if (m_MainCamera.WorldToScreenPoint(transform.position).y > m_MainCamera.pixelHeight || m_MainCamera.WorldToScreenPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            OnHit(50);//collision.gameObject.GetComponent<enemy>().scoreOnHit);
            Destroy(gameObject);
        }
        
    }
}
