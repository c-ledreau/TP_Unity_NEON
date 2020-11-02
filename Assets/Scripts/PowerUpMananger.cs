using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMananger : MonoBehaviour
{
    [SerializeField]
    private PowerUp m_powerUp;
    [SerializeField]
    private float m_spawningTime;
    [SerializeField]
    private Player m_spaceship;
    [SerializeField]
    private Camera m_mainCamera;

    private float height;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
        Debug.Log("test");
        height = m_mainCamera.WorldToScreenPoint(m_spaceship.transform.position).z;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Test()
    {
        while (Application.isPlaying)
        {
            Vector3 foePos = m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(m_mainCamera.pixelWidth * 0.1f, m_mainCamera.pixelWidth * 0.9f), m_mainCamera.pixelHeight, height));
            m_powerUp.m_mainCamera = m_mainCamera;
            m_powerUp.m_spaceShip = m_spaceship;
            m_powerUp.transform.position = foePos;
            Instantiate(m_powerUp);
            yield return new WaitForSeconds(m_spawningTime);
        }
    }

}
