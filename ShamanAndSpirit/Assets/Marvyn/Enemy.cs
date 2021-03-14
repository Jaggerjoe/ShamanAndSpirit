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
    private MeshRenderer m_Renderer;
    [SerializeField]
    private Material m_StunMaterial;
    [SerializeField]
    private Material m_DefaultMaterial;

    [SerializeField]
    private ParticleSystem m_EnemyDeathParticle;

    [SerializeField]
    float m_AttackRate = 0;

    private bool CanMove = true;
    private bool CanAttack = true;
    [SerializeField]
    private GameObject m_AttackPoint;
    [SerializeField]
    private LayerMask m_MaskToDetect;
    [SerializeField]
    private GameObject m_PrefabFeedback;
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
        if(CanMove)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, m_Target.transform.position, (m_Speed * Time.deltaTime));

        }
        
    }

    public void AttackRange()
    {
        if (CanAttack)
        {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_AttackRange, m_MaskToDetect);

            if (hitColliders.Length >= 1)
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
        
        
    }

    public void Attack(Collider p_AttackedEntity)
    {
        CanMove = false;
        
        if (CanAttack)
        {

            if (m_AttackRate <= 1.5)
            {
                m_AttackRate += Time.deltaTime;

            }
            else
            {
                Instantiate(m_PrefabFeedback, m_AttackPoint.transform.position, Quaternion.identity);
                m_AttackRate = 0;
                p_AttackedEntity.GetComponent<Health>().TakeDamages(10.0f);

            }
        }
    }

    private void Update()
    {
        Move();
        AttackRange();
        transform.LookAt(m_Target);
    }

    public void GetStunned(float p_StunDuration)
    {
        CanAttack = false;
        CanMove = false;
        m_Renderer.material = m_StunMaterial;
        Invoke(nameof(GetUnStunned), p_StunDuration);
    }

    private void GetUnStunned()
    {
        CanAttack = true;
        CanMove = true;
        m_Renderer.material = m_DefaultMaterial;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
    }
    private void Die()
    {
        Instantiate(m_EnemyDeathParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }
}
