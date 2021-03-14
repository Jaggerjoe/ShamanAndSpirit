using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private bool m_CanSpawn = false;
    private PlayerMovement m_Shaman = null;

    [SerializeField]
    private float m_SpawnRange = 2.5f;
    [SerializeField]
    private float m_DetectionRange = 3.5f;
    [SerializeField]
    private float m_TimeBeforeSpawn;
    private float m_CurrentTimeBeforeSpawn;

    [SerializeField]
    private GameObject m_EnemyToInstantiate;

    [SerializeField]
    private int m_NbEnemyToSpawn;
    private int m_NbEnemySpawned;

    private void Awake()
    {
        PlayerMovement[] m_Players = FindObjectsOfType<PlayerMovement>();
        foreach (PlayerMovement l_Player in m_Players)
        {
            if (l_Player.tag == "Shaman")
            {
                m_Shaman = l_Player;
            }
        }
    }
    private void Update()
    {
        if (Vector3.Distance(m_Shaman.transform.position, transform.position) <= m_DetectionRange)
        {
            m_CanSpawn = true;
        }
        if (m_CanSpawn)
        {
            if (m_CurrentTimeBeforeSpawn <= m_TimeBeforeSpawn)
            {
                m_CurrentTimeBeforeSpawn += Time.deltaTime;
            }
            else
            {
                m_CurrentTimeBeforeSpawn = 0;

                if (m_NbEnemySpawned < m_NbEnemyToSpawn)
                {
                    Vector3 l_RandomPosition = Random.insideUnitSphere * m_SpawnRange;
                    l_RandomPosition.y = 0.0f;
                    Instantiate(m_EnemyToInstantiate, transform.position + l_RandomPosition, Quaternion.identity);
                    m_NbEnemySpawned += 1;
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_SpawnRange);
        Gizmos.DrawWireSphere(transform.position, m_DetectionRange);
    }
}
