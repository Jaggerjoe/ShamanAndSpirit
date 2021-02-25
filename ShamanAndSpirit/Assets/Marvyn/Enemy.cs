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

    public void Start()
    {
        m_Target = FindObjectOfType<PlayerManager>().transform;
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

                Attack();


            }
        }
        else
        {
            CanMove = true;
        }
        
        
    }

    public void Attack()
    {
        CanMove = false;
        

        if (m_AttackRate <= 1.5)
        {
            m_AttackRate += Time.deltaTime;
            
        }
        else
        {
            m_AttackRate = 0;
            Debug.Log("hit");

        }
    }

    private void Update()
    {
        Move();
        AttackRange();
    }

    private void GetStunned()
    {
        CanAttack = false;
        CanMove = false;
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
