using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class describe the behavior of the first (and for now only) boss
/// </summary>
public class Boss1 : enemy
{
    private Transform weak1; // the first weakness of the boss 
    private Transform weak2; // the first weakness of the boss 
    [SerializeField]
    private GameObject las; //the gameObject that emits the lazer
    private LineRenderer m_line; //the line that render the lazer
    private ParticleSystem m_load; //the Particlesystem taht render the loading of the lazer
    [SerializeField]
    private AudioSource lazer; //the audio of the lazer
    [SerializeField]
    private managerManager m_Manager; //manages the steps

    public bool toto = false;

    public delegate void BossDeath();
    public static event BossDeath Udead;

    int dir;
    bool test;

    protected override void Awake() //initialization of its features
    {
        setCurrentPV(m_MaxPV);
        test = true;
        lazer = GameObject.FindGameObjectWithTag("bossMaterial").GetComponent<AudioSource>();
        m_Manager = FindObjectOfType<managerManager>();
    }

    // Start is called before the first frame update
    void Start() //initialization of its features
    {
        weak1 = gameObject.transform.GetChild(0);
        weak2 = gameObject.transform.GetChild(1);
        dir = 1;
        explosion = transform.GetComponent<ParticleSystem>();
        m_line = las.transform.GetComponent<LineRenderer>();
        m_load = las.transform.GetComponent<ParticleSystem>();
        //scoreOnDestruct = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if ((getCurrentPV() <= m_MaxPV/2 )) // checks when to activate the lazer
        {
            toto = true;
            StartCoroutine(Death());
        }
        isDead();
    }

    IEnumerator Death() //coroutine that start the lazer process
    {
        m_load.Play(); //start the particle system
        lazer.Play(); //start the sound of the lazer
        Debug.Log("enter death coroutine");
        yield return new WaitForSeconds(3.5f);
        m_load.enableEmission = false;  //end the particle system
        float laserTime = 0;
        
        while(laserTime < 3)
        {
            laserTime += Time.deltaTime;
            m_line.SetPosition(0, las.transform.position);                          //set the positions of the lazer
            m_line.SetPosition(1, las.transform.position + new Vector3(0, 0, -100));//
            RaycastHit hit;
            if (Physics.Raycast(las.transform.position, las.transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity) && test)
            {
                if (hit.collider.gameObject.tag == "Player")//checks if the lazer hit the hit, if yes, removes it 50 HP
                {
                    Player pl = hit.collider.transform.GetComponent<Player>();
                    pl.setCurrentPV(pl.getCurrentPV() - 50);
                    pl.UpdateHealthSlider();
                    test = false;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        
        test = false;
        m_line.enabled = false; //disable the line renderer
    }

    /// <summary>
    /// describe the movement of the boss,
    /// </summary>
    override protected void move()
    {
        if (m_mainCamera.WorldToScreenPoint(transform.position).y < m_mainCamera.pixelHeight *0.9) //then it goes right and left
        {
            if(m_mainCamera.WorldToScreenPoint(transform.position).x < m_mainCamera.pixelWidth * 0.2)
            {
                dir = 1;
            }
            if (m_mainCamera.WorldToScreenPoint(transform.position).x > m_mainCamera.pixelWidth * 0.6 + m_mainCamera.pixelWidth * 0.2)
            {
                dir = -1;
            }
            transform.position += Vector3.right * Time.deltaTime * 5 * dir;
        }
        else // it first goes back until it reaches 90% of the screen
        {
            transform.position += Vector3.back * Time.deltaTime*4;
        }
    }


    private bool réparerLesBétisesDeClément = true; // well...

    protected override void  isDead()
    {
        if (getCurrentPV() <= 0 && réparerLesBétisesDeClément)
        {
            Udead();
            m_Manager.m_enemyManager.gameObject.SetActive(true);
            m_Manager.m_enemyManager.Launch();
            Debug.Log("est ce que ça boucle ?");
            if (lazer.isPlaying)
            {
                lazer.Stop();
            }
            réparerLesBétisesDeClément = false;
        }
        base.isDead();
    }
}
