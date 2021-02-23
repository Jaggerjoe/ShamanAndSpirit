using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellsCasting : MonoBehaviour
{
    private PlayerInput m_SpellsInput = null;

    private InputAction m_Aim = null;
    private InputAction m_CastSpell1 = null;
    private InputAction m_CastSpell2 = null;
    private InputAction m_CastSpell3 = null;
    private InputAction m_CastSpell4 = null;

    private Vector2 m_AimDirection = Vector2.zero;

    [SerializeField]
    private SO_Spells m_Spell1 = null;
    private float m_CurrentSpell1CoolDown = 0.0f;
    [SerializeField]
    private SO_Spells m_Spell2 = null;
    private float m_CurrentSpell2CoolDown = 0.0f;
    [SerializeField]
    private SO_Spells m_Spell3 = null;
    private float m_CurrentSpell3CoolDown = 0.0f;
    [SerializeField]
    private SO_Spells m_Spell4 = null;
    private float m_CurrentSpell4CoolDown = 0.0f;

    void Update()
    {
        if (m_SpellsInput == null)
        {
            m_SpellsInput = GetComponent<PlayerInput>();
            m_Aim = m_SpellsInput.actions["Aim"];
            m_CastSpell1 = m_SpellsInput.actions["CastSpell1"];
            m_CastSpell2 = m_SpellsInput.actions["CastSpell2"];
            m_CastSpell3 = m_SpellsInput.actions["CastSpell3"];
            m_CastSpell4 = m_SpellsInput.actions["CastSpell4"];
        }

        m_AimDirection = m_Aim.ReadValue<Vector2>();
        CheckSpell(m_CastSpell1, ref m_CurrentSpell1CoolDown, m_Spell1);
        CheckSpell(m_CastSpell2, ref m_CurrentSpell2CoolDown, m_Spell2);
        CheckSpell(m_CastSpell3, ref m_CurrentSpell3CoolDown, m_Spell3);
        CheckSpell(m_CastSpell4, ref m_CurrentSpell4CoolDown, m_Spell4);
    }

    private void CheckSpell(InputAction p_SpellInput, ref float p_CurrentSpellCoolDown, SO_Spells p_CastSpell)
    {
        if (p_CurrentSpellCoolDown > 0.0f)
        {
            p_CurrentSpellCoolDown = p_CurrentSpellCoolDown - Time.deltaTime;

        }
        else if (p_SpellInput.ReadValue<float>() != 0)
        {
            p_CurrentSpellCoolDown = p_CastSpell.CastSpell(this, m_AimDirection);
        }
    }

    //Pour une raison qui m'échappe, j'ai pas besoin de ça...
    //public IEnumerator CreateProjectiles(SO_Spells p_Spell, SpellsCasting p_PlayerCasting, Vector2 p_AimDirection)
    //{
    //    float l_CurrentTimer = 0.0f;
    //    for (int i = 0; i < p_Spell.Quantity; i++)
    //    {
    //        while (l_CurrentTimer > 0.0f)
    //        {
    //            yield return null;
    //        }
    //        l_CurrentTimer = p_Spell.TimeBetweenProjectiles;
    //        Vector3 l_ProjectilePosition = new Vector3(p_PlayerCasting.transform.position.x + p_AimDirection.x, p_PlayerCasting.transform.position.y, p_PlayerCasting.transform.position.z + p_AimDirection.y);
    //        GameObject l_Projectile = Instantiate(p_Spell.ProjectilePrefab, l_ProjectilePosition, Quaternion.identity);
    //        l_Projectile.GetComponent<Projectile>().SetProjectile(p_Spell.AffectedEntities, p_Spell.Speed, p_AimDirection, p_Spell.ExplosiveProjectile, p_Spell.ExplosionRange);
    //    }
    //}
}
