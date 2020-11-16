using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : Entity
{
    [SerializeField]
    private float m_enemySpeed;
    [SerializeField]
    public Camera m_mainCamera;
    [SerializeField]
    private float scoreOnDestruct = 50;
    protected ParticleSystem explosion;

    [SerializeField]
    private float powSpawnProba = .2f;

    public delegate void OnHitAction(float addedScore);
    public static event OnHitAction OnDestruct;

    public delegate void SpamwPowAction(Vector3 foePos);
    public static event SpamwPowAction spawn;


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

    protected virtual void isDead()
    {
        if (getCurrentPV() <= 0)
        {
            float toto = Random.Range(0, 101);
            if (powSpawnProba*100 >= toto)
            {
                //Debug.Log(toto);
                spawn(transform.position);
            }
            OnDestruct(scoreOnDestruct);
            explosion.Play();
            for (int k =0; k< gameObject.transform.childCount; k++)
                gameObject.transform.GetChild(k).gameObject.SetActive(false);
            Destroy(transform.GetComponent<MeshRenderer>());
            Destroy(transform.GetComponent<MeshFilter>());
            Destroy(transform.GetComponent<Collider>());
            StartCoroutine(Wait());

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

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
        if (collision.gameObject.tag == "Player")
        {
            setCurrentPV(getCurrentPV() - 100);
        }

        if (collision.gameObject.tag == "bullet")
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
