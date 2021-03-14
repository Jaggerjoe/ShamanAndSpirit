using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    [SerializeField]
    private float m_MaxMana;

    [SerializeField]
    private float m_CurrentMana;

    public PlayerManaData m_PlayerM;



    private void Start()
    {
        m_MaxMana = 100;
        m_CurrentMana = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ManaBall" && m_CurrentMana < 100)
        {
            Debug.Log("ouké1");

            m_CurrentMana += 20;
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Shaman" && m_CurrentMana >= 0 && m_PlayerM.m_CurrentMana < m_PlayerM.m_MaxMana )
        {
            Debug.Log("ouké");
            m_PlayerM.m_CurrentMana += 10 * Time.deltaTime;
            m_CurrentMana -= 10 * Time.deltaTime;
          

        }
    }

    private void Update()
    {
      
    }

}
