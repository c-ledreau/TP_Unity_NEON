using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            transform.Rotate(30, 0, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Update is called once per frame
}
