using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroyparticle : MonoBehaviour
{
    private ParticleSystem m_Particles = null;
    private void Awake()
    {
        m_Particles = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        Invoke(nameof(DestroyGameObject), m_Particles.main.duration + m_Particles.main.startLifetimeMultiplier);
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
