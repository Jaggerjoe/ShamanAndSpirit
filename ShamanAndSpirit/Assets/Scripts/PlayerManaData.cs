using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaData : MonoBehaviour
{
    public float m_MaxMana = 100.0f;

    public float m_CurrentMana = 40.0f;

    public float MaxMana => m_MaxMana;
    public float CurrentMana => m_CurrentMana;
}
