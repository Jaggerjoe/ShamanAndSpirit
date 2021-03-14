using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellsCasting : MonoBehaviour
{
    [SerializeField]
    private bool m_DisplayUI = false;

    private PlayerInput m_SpellsInput = null;

    private InputAction m_Aim = null;
    private InputAction m_CastSpell1 = null;
    private InputAction m_CastSpell2 = null;
    private InputAction m_CastSpell3 = null;
    private InputAction m_CastSpell4 = null;

    private Vector3 m_AimDirection = Vector2.zero;

    [Header("Gachette droite")]
    [SerializeField]
    private SO_Spells m_Spell1 = null;
    private float m_CurrentSpell1CoolDown = 0.0f;
    [Header("Bouton droit")]
    [SerializeField]
    private SO_Spells m_Spell2 = null;
    private float m_CurrentSpell2CoolDown = 0.0f;
    [Header("Gachette gauche")]
    [SerializeField]
    private SO_Spells m_Spell3 = null;
    private float m_CurrentSpell3CoolDown = 0.0f;
    [Header("Bouton gauche")]
    [SerializeField]
    private SO_Spells m_Spell4 = null;
    private float m_CurrentSpell4CoolDown = 0.0f;

    [SerializeField]
    private Image R1Spell = null;
    [SerializeField]
    private Image R2Spell = null;
    [SerializeField]
    private Image L1Spell = null;
    [SerializeField]
    private Image L2Spell = null;

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

        m_AimDirection.x = m_Aim.ReadValue<Vector2>().x;
        m_AimDirection.z = m_Aim.ReadValue<Vector2>().y;
        m_AimDirection.y = 0.0f;
        
        CheckSpell(m_CastSpell1, ref m_CurrentSpell1CoolDown, m_Spell1, R2Spell);
        CheckSpell(m_CastSpell2, ref m_CurrentSpell2CoolDown, m_Spell2, R1Spell);
        CheckSpell(m_CastSpell3, ref m_CurrentSpell3CoolDown, m_Spell3, L2Spell);
        CheckSpell(m_CastSpell4, ref m_CurrentSpell4CoolDown, m_Spell4, L1Spell);
    }

    private void CheckSpell(InputAction p_SpellInput, ref float p_CurrentSpellCoolDown, SO_Spells p_CastSpell, Image p_ImageCD)
    {
        if (p_CurrentSpellCoolDown > 0.0f)
        {
            p_CurrentSpellCoolDown = p_CurrentSpellCoolDown - Time.deltaTime;
            if (m_DisplayUI)
            {
                Color l_Color = p_ImageCD.color;
                l_Color.a = 0.10f;
                p_ImageCD.color = l_Color;
            }
        }
        else if (p_SpellInput.ReadValue<float>() != 0 && p_CastSpell.ManaCost <= GetComponent<PlayerManaData>().m_CurrentMana && m_AimDirection != Vector3.zero)
        {
            GetComponent<PlayerManaData>().m_CurrentMana = GetComponent<PlayerManaData>().m_CurrentMana - p_CastSpell.ManaCost;
            p_CurrentSpellCoolDown = p_CastSpell.CastSpell(this, m_AimDirection);
        }
        else
        {
            if (m_DisplayUI)
            {
                Color l_Color = p_ImageCD.color;
                l_Color.a = 1.0f;
                p_ImageCD.color = l_Color;
            }
        }
    }
}
