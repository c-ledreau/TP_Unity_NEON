using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField]
    private enemy m_foe;
    [SerializeField]
    private shootingEnemy m_shootingFoe;
    [SerializeField]
    private float m_spawningTime;
    [SerializeField]
    private GameObject m_spaceship;
    [SerializeField]
    private Camera m_mainCamera;

    private float height;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pattern());
        StartCoroutine(Alea());
        height = m_mainCamera.WorldToScreenPoint(m_spaceship.transform.position).z;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Alea()
    {
        while (Application.isPlaying)
        {
            Debug.Log("test");
            Vector3 foePos = m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(m_mainCamera.pixelWidth * 0.1f, m_mainCamera.pixelWidth * 0.9f), m_mainCamera.pixelHeight, height));
            m_foe.m_mainCamera = m_mainCamera;
            m_foe.transform.position = foePos;

            Vector3 shootingFoePos = m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(m_mainCamera.pixelWidth * 0.1f, m_mainCamera.pixelWidth * 0.9f), m_mainCamera.pixelHeight, height));
            m_shootingFoe.m_mainCamera = m_mainCamera;
            m_shootingFoe.transform.position = shootingFoePos;

            Instantiate(m_foe);
            yield return new WaitForSeconds(Random.Range(0.5f,1.5f)* m_spawningTime);
            Instantiate(m_shootingFoe);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f) * m_spawningTime);
        }

    }

    IEnumerator Pattern()
    {
        while (Application.isPlaying)
        {
            int i = Random.Range(0, 4);
            int k = 11;
            switch (i)
            {
                case 0:
                    for(int j = 0; j < k; j++)
                    {
                        Vector3 Pos = m_mainCamera.ScreenToWorldPoint(new Vector3((j+0.5f)*m_mainCamera.pixelWidth/k, m_mainCamera.pixelHeight, height));
                        m_foe.m_mainCamera = m_mainCamera;
                        m_foe.transform.position = Pos;
                        Instantiate(m_foe);
                    }
                    break;
                case 1:
                    for (int j = 0; j < k; j++)
                    {
                        Vector3 Pos = m_mainCamera.ScreenToWorldPoint(new Vector3((j + 0.5f) * m_mainCamera.pixelWidth / k, m_mainCamera.pixelHeight+Mathf.Abs(k/2-j)* 50, height));
                        m_foe.m_mainCamera = m_mainCamera;
                        m_foe.transform.position = Pos;
                        Instantiate(m_foe);
                    }
                    break;
                case 2:
                    for (int j = 0; j < k; j++)
                    {
                        Vector3 Pos = m_mainCamera.ScreenToWorldPoint(new Vector3((Mathf.Cos(j*(360/k))+1)/2 * m_mainCamera.pixelWidth, m_mainCamera.pixelHeight + (Mathf.Sin(j * (360 / k)) + 1) / 4* m_mainCamera.pixelHeight, height));
                        m_foe.m_mainCamera = m_mainCamera;
                        m_foe.transform.position = Pos;
                        Instantiate(m_foe);
                    }
                    break;
                case 3:
                    for (int j = 0; j < k; j++)
                    {
                        Vector3 Pos = m_mainCamera.ScreenToWorldPoint(new Vector3((Mathf.Cos(j * (360 / k)) + 1) / 2 * m_mainCamera.pixelWidth/2 + m_mainCamera.pixelWidth / 4, j*m_mainCamera.pixelHeight/20+ m_mainCamera.pixelHeight, height));
                        m_foe.m_mainCamera = m_mainCamera;
                        m_foe.transform.position = Pos;
                        Instantiate(m_foe);
                    }
                    break;
                case 4:
                    break;
            }
            yield return new WaitForSeconds(m_spawningTime*5);
        }
    }
}
