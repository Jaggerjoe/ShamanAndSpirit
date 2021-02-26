using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaData : MonoBehaviour
{
   public float m_MaxMana;

   public float m_CurrentMana;

    public float MaxMana => m_MaxMana;
    public float CurrentMana => m_CurrentMana;

    private void Start()
    {
        m_MaxMana = 100;
        m_CurrentMana = 40;
    }
}
