using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActive : MonoBehaviour
{
    public GameObject gameobject;

    public void Activate()
    {
        gameobject.SetActive(true);
    }

    public void Desactivate()
    {
        gameobject.SetActive(false);

    }
}
