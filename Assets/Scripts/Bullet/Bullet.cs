using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the behaviour of the bullet
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_bulletSpeed; //speed of the bullet
    [SerializeField]
    public Camera m_MainCamera;
    public float m_angle;
    [SerializeField]
    private int m_damage = 5; //damage of the bullet
    public float m_direction; //direction of the bullet

    private bool FromPlayer;


    //TEST MEMBERS
    private float time = 0;

    public delegate void OnHitAction(float scoreHit);
    public static event OnHitAction OnHit;

    //pattern of the bullets
    public enum patterns
    {
        Base,
        Sinus,
    }

    public patterns pattern;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        StartCoroutine(falseTime());
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

    /// <summary>
    /// manages the movement of the bullet, regarding if it is shot by an enemy or the player
    /// </summary>
    private void move()
    {
        transform.position += bulletPattern(pattern);
        if (m_MainCamera.WorldToScreenPoint(transform.position).y > m_MainCamera.pixelHeight || m_MainCamera.WorldToScreenPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy" && FromPlayer == true)
        {
            //OnHit(collision.gameObject.GetComponent<enemy>().scoreOnHit);
            Destroy(gameObject);
        }

    }

    public void setOrigine(bool origine)
    {
        FromPlayer = origine;
    }

    public bool isFromPlayer()
    {
        return FromPlayer;
    }

    /// <summary>
    /// define the pattern of the bullt
    /// </summary>
    /// <param name="pattern">Sinus or Base</param>
    /// <returns></returns>
    private Vector3 bulletPattern(patterns pattern)
    {
        Vector3 res;// = new Vector3(m_angle, 0.0f, m_direction * .7f) * Time.deltaTime * m_bulletSpeed; ;

        switch (pattern)
        {
            case patterns.Sinus:
                res = new Vector3(m_angle + Mathf.Sin(time * 25), 0.0f, m_direction * .7f) * Time.deltaTime * m_bulletSpeed;
                return res;

            case patterns.Base:
                res = new Vector3(m_angle, 0.0f, m_direction * .7f) * Time.deltaTime * m_bulletSpeed;
                return res;

            default:
                res = new Vector3(m_angle, 0.0f, m_direction * .7f) * Time.deltaTime * m_bulletSpeed;
                return res;
        }
    }


    IEnumerator falseTime()
    {

        while (isActiveAndEnabled)
        {
            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }
}
