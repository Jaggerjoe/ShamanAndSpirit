using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Scriptable objects/NewSpell")]
public class SO_Spells : ScriptableObject
{

    [Header("All spells")]
    [SerializeField]
    private SpellType m_SpellType;
    [SerializeField]
    private float m_ManaCost = 10.0f;
    public float ManaCost { get { return m_ManaCost; } }
    [SerializeField]
    private float m_CoolDown = 1.0f;
    [SerializeField]
    private LayerMask m_AffectedEntities;
    public LayerMask AffectedEntities { get { return m_AffectedEntities; } }
    [SerializeField]
    private float m_Damages = 1.0f;
    [SerializeField]
    private float m_Range = 5.0f;

    [Header("Projectiles")]
    [SerializeField]
    private GameObject m_ProjectilePrefab = null;
    public GameObject ProjectilePrefab { get { return m_ProjectilePrefab; } }
    [SerializeField]
    private float m_Speed = 5.0f;
    public float Speed { get { return m_Speed; } }
    [SerializeField]
    private int m_Quantity = 1;
    public int Quantity { get { return m_Quantity; } }
    [SerializeField]
    private float m_TimeBetweenProjectiles = 0.5f;
    public float TimeBetweenProjectiles { get { return m_TimeBetweenProjectiles; } }
    [SerializeField]
    private bool m_ExplosiveProjectile = false;
    public bool ExplosiveProjectile { get { return m_ExplosiveProjectile; } }
    [SerializeField]
    private float m_ExplosionRange;
    public float ExplosionRange { get { return m_ExplosionRange; } }

    [Header("AOEs")]
    [SerializeField]
    private float m_Angle = 45.0f;

    [Header("Secondary effects")]
    [SerializeField]
    private List<SecondaryEffects> m_SecondaryEffects = new List<SecondaryEffects>();
    [SerializeField]
    private float m_StunDuration = 2.0f;
    [SerializeField]
    private float m_KnockbackValue = 1.0f;

    [Header("Feedbacks")]
    [SerializeField]
    private GameObject m_ParticlePrefab = null;


    public float CastSpell(SpellsCasting p_PlayerCasting, Vector3 p_AimDirection)
    {
        switch (m_SpellType)
        {
            case SpellType.Projectile:
                p_PlayerCasting.StartCoroutine(CreateProjectiles(p_PlayerCasting, p_AimDirection));
                break;
            case SpellType.AOE:
                if (m_ParticlePrefab != null)
                {
                    GameObject l_Particles = null;
                    l_Particles = Instantiate(m_ParticlePrefab, p_PlayerCasting.transform.position, Quaternion.identity);
                    var l_shapeModule = l_Particles.GetComponent<ParticleSystem>().shape;
                    l_shapeModule.arc = m_Angle;
                    var l_MainModule = l_Particles.GetComponent<ParticleSystem>().main;
                    l_MainModule.startLifetime = m_Range / l_Particles.GetComponent<ParticleSystem>().main.startSpeedMultiplier;
                    l_Particles.transform.forward = p_AimDirection;
                    l_Particles.transform.Rotate(0.0f, (m_Angle - 180.0f) / 2.0f, 0.0f);
                }

                Collider[] l_AffectedEntities = Physics.OverlapSphere(p_PlayerCasting.transform.position, m_Range, m_AffectedEntities);
                foreach (Collider l_AffectedEntity in l_AffectedEntities)
                {
                    if (l_AffectedEntity.GetComponent<Enemy>() != null || l_AffectedEntity.GetComponent<EnemyArcher>() != null)
                    {
                        ApplySpellEffect(l_AffectedEntity, p_PlayerCasting, p_AimDirection);
                    }
                }
                break;
            case SpellType.Dash:
                GameObject l_PlayerObject = p_PlayerCasting.gameObject;
                p_AimDirection.Normalize();
                if (!Physics.Raycast(p_PlayerCasting.transform.position, p_AimDirection, m_Range, m_AffectedEntities))
                {
                    l_PlayerObject.transform.Translate(p_AimDirection * m_Range, Space.World);
                }
                break;
        }
        return m_CoolDown;
    }


    public IEnumerator CreateProjectiles(SpellsCasting p_PlayerCasting, Vector3 p_AimDirection)
    {
        float l_CurrentTimer = 0.0f;
        for (int i = 0; i < m_Quantity; i++)
        {
            while (l_CurrentTimer > 0.0f)
            {
                l_CurrentTimer = l_CurrentTimer - Time.deltaTime;
                yield return null;
            }
            l_CurrentTimer = m_TimeBetweenProjectiles;
            Vector3 l_ProjectilePosition = p_PlayerCasting.transform.position + p_AimDirection;
            GameObject l_Projectile = Instantiate(m_ProjectilePrefab, l_ProjectilePosition, Quaternion.identity);
            l_Projectile.transform.forward = p_AimDirection;
            l_Projectile.GetComponent<Projectile>().SetProjectile(m_AffectedEntities, m_Speed, p_AimDirection, m_ExplosiveProjectile, m_ExplosionRange, m_Damages);
        }
    }

    public void ApplySpellEffect(Collider p_AffectedEntity, SpellsCasting p_PlayerCasting, Vector3 p_AimDirection)
    {
        float l_Angle = Vector3.Angle(p_AimDirection.normalized, (p_AffectedEntity.transform.position - p_PlayerCasting.transform.position).normalized);
        if (l_Angle <= m_Angle / 2.0f)
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

            foreach (SecondaryEffects l_Effect in m_SecondaryEffects)
            {
                switch (l_Effect)
                {
                    case SecondaryEffects.KnockBack:
                        Vector3 l_KnockBackDirection = p_AffectedEntity.transform.position - p_PlayerCasting.transform.position;
                        l_KnockBackDirection.Normalize();
                        l_KnockBackDirection.y = 0.0f;

                        if(Physics.Raycast(p_AffectedEntity.transform.position, l_KnockBackDirection, out RaycastHit l_HitInfo, m_KnockbackValue))
                        {
                            m_KnockbackValue = Vector3.Distance(l_HitInfo.point, p_AffectedEntity.transform.position);
                        }

                        p_AffectedEntity.transform.Translate(l_KnockBackDirection * m_KnockbackValue, Space.World);
                        break;
                    case SecondaryEffects.Stun:
                        if (p_AffectedEntity.TryGetComponent<Enemy>(out Enemy p_Enemy))
                        {
                            p_Enemy.GetStunned(m_StunDuration);
                        }
                        else if (p_AffectedEntity.TryGetComponent<EnemyArcher>(out EnemyArcher p_EnemyArcher))
                        {
                            p_EnemyArcher.GetStunned(m_StunDuration);
                        }
                        break;

                }
            }
        }
    }
}

public enum SpellType
{
    Projectile,
    AOE,
    Dash,
}

public enum SecondaryEffects
{
    Stun,
    KnockBack,
}
