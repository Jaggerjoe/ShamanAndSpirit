using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    [SerializeField]
    private float m_MaxMana;

    [SerializeField]
    private Slider m_ManaSliderPlayer;

    [SerializeField]
    private Slider m_ManaSliderSpirit;

    [SerializeField]
    private ParticleSystem m_ManaParticle;
    [SerializeField]
    private ParticleSystem m_ManaParticlePlayer;

    [SerializeField]
    private float m_CurrentMana;

    public PlayerManaData m_PlayerM;



    private void Start()
    {
        m_MaxMana = 100;
        m_CurrentMana = 0;
        m_ManaParticle.Stop();
        m_ManaParticlePlayer.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ManaBall" && m_CurrentMana < m_MaxMana)
        {
            
            Debug.Log("ouké1");

            m_CurrentMana += 20;
            Destroy(other.gameObject);
            m_ManaParticle.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Shaman" && m_CurrentMana >= 0 && m_PlayerM.m_CurrentMana < m_PlayerM.m_MaxMana )
        {
            Debug.Log("ouké");
            m_PlayerM.m_CurrentMana += 10 * Time.deltaTime;
            m_CurrentMana -= 10 * Time.deltaTime;
            m_ManaParticlePlayer.Play();

        }
    }

    private void Update()
    {
        m_ManaSliderSpirit.value = m_CurrentMana;
        m_ManaSliderPlayer.value = m_PlayerM.m_CurrentMana;
    }

}
