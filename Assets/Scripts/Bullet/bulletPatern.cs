using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPatern : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual Vector3 posUpdate(float p_angle, float p_direction, float p_speed)
    {
        return new Vector3(p_angle, 0.0f, p_direction) * Time.deltaTime * p_speed;
    }
}
