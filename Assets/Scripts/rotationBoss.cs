using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// make the 2 parts of the boss rotates
/// </summary>
public class rotationBoss : MonoBehaviour
{
    [SerializeField]
    private bool one;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (one)
        {
            transform.Rotate(0, 0.5f * Time.timeScale, 0);
        }
        else
        {
            transform.Rotate(0, -1 * Time.timeScale, 0);
        }
    }
}
