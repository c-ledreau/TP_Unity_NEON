using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this classe makes rotate the spaceships for the selection menu and the little one in the bottom right corner
/// </summary>
public class rotation : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Rot());
    }

    IEnumerator Rot()
    {
        while (Application.isPlaying)
        {
            transform.Rotate(0, 30, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Update is called once per frame
}
