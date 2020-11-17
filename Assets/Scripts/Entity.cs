using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is the base for every enetity in the game
/// </summary>
public class Entity : MonoBehaviour
{
    public int m_MaxPV; //the maximum number of HP the entity can have
    protected float m_bulletScale; // size of a bullet from the entity
    protected float m_speedBullet; // speed of a bullet from the entity
    [SerializeField]
    protected int m_nbrGun; // number of gun of the entity
    protected int m_dmg; // damage of a bullet
    public float m_fireRate; //fire rate of the entity
    [SerializeField]
    protected Bullet bullet;
    [SerializeField]
    private int CurrentPV; //current HP of the entity


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

    /// <summary>
    /// increase the size of a bullet from the entity by 5
    /// </summary>
    public void IncreaseBulletScale()
    {
        m_bulletScale += 5.0f;
    }

    /// <summary>
    /// increase the fire rate of the entity by 1
    /// </summary>
    public void IncreaseFireRate()
    {
        m_fireRate += 1.0f;
    }

    /// <summary>
    /// increase the speed of a bullet from the entity by 1
    /// </summary>
    public void IncreaseSpeedBullet()
    {
        m_speedBullet += 1.0f;
    }

    /// <summary>
    /// increse the number of gun of the entity by 1
    /// </summary>
    public void IncreaseNbrGun()
    {
        m_nbrGun += 1;
    }

    /// <summary>
    /// increase the damage of a bullet from the entity
    /// </summary>
    public void IncreaseDmg()
    {
        m_dmg += 3;
    }

    /// <summary>
    /// heal the entity by 10 with a limit of m_MaxPV
    /// </summary>
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
