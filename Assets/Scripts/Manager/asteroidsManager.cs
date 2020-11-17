using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// behavior of the asteroids
/// </summary>
public class asteroidsManager : MonoBehaviour
{
    [SerializeField]
    private Asteroids m_ast; //asteroid prefab
    [SerializeField]
    private float m_spawningTime; //spawning time between each asteroids
    [SerializeField]
    private Camera m_mainCamera; 
    [SerializeField]
    private GameObject m_spaceship;

    private float height; //hight of the player 

    void Start() //initialize the features
    {
        height = m_mainCamera.WorldToScreenPoint(m_spaceship.transform.position).z;
        StartCoroutine(Alea());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Alea() //randomly spawn the asteroids
    {
        while (Application.isPlaying)
        {
            Vector3 astPos = m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(0, m_mainCamera.pixelWidth), m_mainCamera.pixelHeight, height+ Random.Range(5, 150)));
            m_ast.m_mainCamera = m_mainCamera;
            m_ast.transform.position = astPos;

            Instantiate(m_ast);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f) * m_spawningTime);
        }

    }
}
