using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float m_MaxHealth = 100.0f;
    private float m_Health = 100.0f;
    [SerializeField]
    private GameObject m_DamagesParticles = null;

    private void Awake()
    {
        m_Health = m_MaxHealth;
    }

    private void Update()
    {
        if (m_Health <= 0.0f)
        {
            if (gameObject.tag == "Shaman" || gameObject.tag == "Spirit")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public float TakeDamages(float p_Damages)
    {
        Instantiate(m_DamagesParticles, transform);
        return m_Health = Mathf.Clamp(m_Health - p_Damages, 0.0f, m_MaxHealth);
    }
}
