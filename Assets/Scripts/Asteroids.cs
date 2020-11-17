using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField]
    private float m_astSpeed; //speed of the asteroids
    [SerializeField]
    public Camera m_mainCamera;//camera of the asteroids

    // Update is called once per frame
    void FixedUpdate()
    {
        moveAst();
    }

    /// <summary>
    /// decribes the movement of the asteroids with a random position ans rotation
    /// </summary>
    void moveAst()
    {
        transform.position += Vector3.back * Time.deltaTime * m_astSpeed;
        transform.Rotate(new Vector3(Random.Range(2, 8), Random.Range(2, 8), Random.Range(2, 8)));
        if (m_mainCamera.WorldToScreenPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }
}
