using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class describe the behavior of the enemies
/// </summary>
public class enemy : Entity
{
    [SerializeField]
    private float m_enemySpeed; //the speed of the enemy
    [SerializeField]
    public Camera m_mainCamera; //the camera of the enemy
    [SerializeField]
    private float scoreOnDestruct = 50; //the score the enemy gives the player if it is detroy
    protected ParticleSystem explosion; //the explosion for the death of the enemy

    [SerializeField]
    private float powSpawnProba = .2f; //the probability the enemy has to drop a powerUp

    public delegate void OnHitAction(float addedScore);
    public static event OnHitAction OnDestruct;

    public delegate void SpamwPowAction(Vector3 foePos);
    public static event SpamwPowAction spawn;


    private bool ClémentIlFautMettreDesConditionsPlusStrictes = false; //well..........

    protected override void Awake()
    {
        setCurrentPV(m_MaxPV);
    }

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetComponent<ParticleSystem>();
    }

    void Update()
    {
       
    }
    /// <summary>
    /// handle the death of the enemy
    /// We use coroutines in order to see the particle system attached to the enemy
    /// </summary>
    protected virtual void isDead()
    {
        if ((getCurrentPV() <= 0) && !ClémentIlFautMettreDesConditionsPlusStrictes)
        {
            ClémentIlFautMettreDesConditionsPlusStrictes = true;
            
            OnDestruct(scoreOnDestruct);
            explosion.Play(); //play the explosion
            for (int k =0; k< gameObject.transform.childCount; k++) //delete all children of the enemy
                gameObject.transform.GetChild(k).gameObject.SetActive(false);
            Destroy(transform.GetComponent<MeshRenderer>());
            Destroy(transform.GetComponent<MeshFilter>());
            Destroy(transform.GetComponent<Collider>());
            //Destroy(gameObject);
            StartCoroutine(Destroy());
        }
    }

    /// <summary>
    ///  destroy the enemy after 0.4 second
    /// </summary>
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.40f);
        float toto = Random.Range(0, 101);
        if (powSpawnProba * 100 >= toto) //handle the spawn of a powerUp in a coroutine
        {
            //Debug.Log(toto);
            spawn(transform.position);
            //StartCoroutine(poweru());
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Spawn a power up after 0.4 second
    /// </summary>
    IEnumerator poweru()
    {
        yield return new WaitForSeconds(0.40f);
        spawn(transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    /// <summary>
    /// make move the enemy to the bottom of the screen and destrroys it when it is out of the range of the camera
    /// </summary>
    virtual protected void move()
    {
        transform.position += Vector3.back * Time.deltaTime * m_enemySpeed;
        if (m_mainCamera.WorldToScreenPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") //the enemy is destroyed if is touches the player
        {
            setCurrentPV(getCurrentPV() - 100);
        }

        if (collision.gameObject.tag == "bullet") //the enemy lost some HP regarding the damage of the bullet it collided with, then detroyed this bullet
        {
            Bullet bull = collision.gameObject.GetComponent<Bullet>();
            if (bull.isFromPlayer())
            {
                setCurrentPV(getCurrentPV() - bull.getDamage());
                
            }
            Destroy(bull.gameObject);
        }
        isDead();
    }
}
