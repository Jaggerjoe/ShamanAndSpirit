using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private LayerMask m_AffectedEntities;
    private float m_Damages;
    private float m_Speed;
    private Vector3 m_Direction;
    private bool m_ExplodeOnImpact = false;
    private float m_ExplosionRange = 5.0f;

    public void SetProjectile(LayerMask p_AffectedEntities, float p_Speed, Vector3 p_Direction, bool p_ExplodeOnImpact, float p_ExplosionRange, float p_Damages)
    {
        m_AffectedEntities = p_AffectedEntities;
        m_Speed = p_Speed;

        m_Direction.x = p_Direction.x;
        m_Direction.y = 0.0f;
        m_Direction.z = p_Direction.y;

        m_ExplodeOnImpact = p_ExplodeOnImpact;
        m_ExplosionRange = p_ExplosionRange;

        m_Damages = p_Damages;
    }

    private void Update()
    {
        if (Physics.SphereCast(transform.position, 1.0f, m_Direction, out RaycastHit l_HitInfo, m_Speed * Time.deltaTime, m_AffectedEntities))
        {
            if (m_ExplodeOnImpact)
            {
                Collider[] l_AffectedEntities = Physics.OverlapSphere(transform.position, m_ExplosionRange, m_AffectedEntities);
                foreach (Collider l_Collider in l_AffectedEntities)
                {
                    CheckDamages(l_Collider.gameObject);
                }
            }
            else
            {
                CheckDamages(l_HitInfo.collider.gameObject);
            }
        }

        transform.Translate(m_Direction.normalized * m_Speed * Time.deltaTime, Space.World);

    }

    private void CheckDamages(GameObject p_AffectedEntity)
    {
        //Infliger des dégâts
        if ((p_AffectedEntity.GetComponent<Enemy>() != null) || (p_AffectedEntity.GetComponent<EnemyArcher>() != null))
        {
            float l_RemainingLife = p_AffectedEntity.GetComponent<Health>().TakeDamages(m_Damages);
            if (l_RemainingLife <= 0.0f)
            {
                return;
            }
        }
    }

}
