using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : enemy
{
    private Transform weak1;
    private Transform weak2;
    [SerializeField]
    private GameObject las;
    private LineRenderer m_line;
    private ParticleSystem m_load;
    [SerializeField]
    private AudioSource lazer;

    public bool toto = false;

    int dir;
    bool test;

    protected override void Awake()
    {
        setCurrentPV(m_MaxPV);
        test = true;
        lazer = GameObject.FindGameObjectWithTag("bossMaterial").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
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
        if (getCurrentPV() <= m_MaxPV/2)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        m_load.Play();
        lazer.Play();
        yield return new WaitForSeconds(3.5f);
        m_load.enableEmission = false;
        m_line.SetPosition(0, las.transform.position);
        m_line.SetPosition(1, las.transform.position + new Vector3(0, 0, -100));
        RaycastHit hit;
        if (Physics.Raycast(las.transform.position,las.transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity) && test)
        {
            if (hit.collider.gameObject.tag =="Player")
            {
                Player pl = hit.collider.transform.GetComponent<Player>();
                pl.setCurrentPV(pl.getCurrentPV() - 50);
                pl.UpdateHealthSlider();
                test = false;
            }
        }
        yield return new WaitForSeconds(3);
        test = false;
        m_line.enabled = false;
    }

    override protected void move()
    {

        if (m_mainCamera.WorldToScreenPoint(transform.position).y < m_mainCamera.pixelHeight *0.9)
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
        else
        {
            transform.position += Vector3.back * Time.deltaTime*4;
        }
    }

    private void testSon()
    {
        if(toto)
        {
            lazer.Play();
            toto = false;
        }
    }
}
