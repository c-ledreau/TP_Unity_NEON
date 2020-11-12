using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private Boss1 m_boss;
    private LineRenderer m_line;
    [SerializeField]
    private GameObject las;
    // Start is called before the first frame update
    void Start()
    {
        m_boss = transform.parent.parent.GetComponent<Boss1>();
        m_line = transform.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_boss.getCurrentPV() <= m_boss.m_MaxPV/2)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.GetChild(0).gameObject.SetActive(false);
        m_line.SetPosition(0, transform.position);
        m_line.SetPosition(1, transform.position+new Vector3(0,0,-100));
    }
}
