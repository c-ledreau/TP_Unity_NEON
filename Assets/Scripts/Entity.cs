using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int m_MaxPV;
    protected float m_bulletScale;
    protected float m_speedBullet;
    [SerializeField]
    protected int m_nbrGun;
    protected int m_dmg;
    public float m_fireRate;
    [SerializeField]
    protected Bullet bullet;
    [SerializeField]
    private int CurrentPV;


    public int getCurrentPV()
    {
        return CurrentPV;
    }

    public void setCurrentPV(int p_currentPV)
    {
        CurrentPV = p_currentPV;
    }

    protected virtual void Awake()
    {
        CurrentPV = m_MaxPV;
    }

    public void IncreaseBulletScale()
    {
        m_bulletScale += 5.0f;
    }

    public void IncreaseFireRate()
    {
        m_fireRate += 1.0f;
    }

    public void IncreaseSpeedBullet()
    {
        m_speedBullet += 1.0f;
    }

    public void IncreaseNbrGun()
    {
        m_nbrGun += 1;
    }

    public void IncreaseDmg()
    {
        m_dmg += 3;
    }

    public void Heal()
    {
        if ((CurrentPV + 10) < m_MaxPV)
        {
            CurrentPV += 10;
        }
        else
        {
            CurrentPV = m_MaxPV;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
