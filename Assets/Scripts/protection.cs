using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protection : enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void move()
    {
        transform.position += Vector3.left * Time.deltaTime * 10;
        if (m_mainCamera.WorldToScreenPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }

}

