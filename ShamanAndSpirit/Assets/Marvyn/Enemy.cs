using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target = null;

    [SerializeField]
    private float m_AttackRange;
    [SerializeField]
    private float m_Speed;

    [SerializeField]
    float m_AttackRate = 0;

    private bool CanMove = true;
    private bool CanAttack = true;

    [SerializeField]
    private LayerMask m_MaskToDetect;

    private float m_CurrentStunDuration = 0.0f;

    public void Start()
    {
        PlayerMovement[] m_Players = FindObjectsOfType<PlayerMovement>();
        foreach (PlayerMovement l_Player in m_Players)
        {
            if (l_Player.tag == "Shaman")
            {
                m_Target = l_Player.transform;
            }
        }
    }

    public void Move()
    {
        if(CanMove == true)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, m_Target.transform.position, (m_Speed * Time.deltaTime));

        }
        
    }

    public void AttackRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_AttackRange, m_MaskToDetect);

        if(hitColliders.Length >= 1)
        {
            foreach (Collider collider in hitColliders)
            {

                Attack(collider);


            }
        }
        else
        {
            CanMove = true;
        }
        
        
    }

    public void Attack(Collider p_AttackedEntity)
    {
        CanMove = false;
        

        if (m_AttackRate <= 1.5)
        {
            m_AttackRate += Time.deltaTime;
            
        }
        else
        {
            m_AttackRate = 0;
            p_AttackedEntity.GetComponent<Health>().TakeDamages(10.0f);

        }
    }

    private void Update()
    {
        Move();
        AttackRange();

        if (m_CurrentStunDuration > 0.0f)
        {
            m_CurrentStunDuration = m_CurrentStunDuration - Time.deltaTime;
        }
        else
        {
            GetUnStunned();
        }
    }

    public void GetStunned(float p_StunDuration)
    {
        CanAttack = false;
        CanMove = false;

        m_CurrentStunDuration = p_StunDuration;
    }

    private void GetUnStunned()
    {
        CanAttack = true;
        CanMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
    }
}
