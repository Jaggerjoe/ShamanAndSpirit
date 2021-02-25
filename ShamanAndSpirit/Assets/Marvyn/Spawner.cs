using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float m_TimeBeforeSpawn;
    private float m_CurrentTimeBeforeSpawn;

    [SerializeField]
    private GameObject m_EnemyToInstantiate;

    [SerializeField]
    private int m_NbEnemyToSpawn;
    private int m_NbEnemySpawned;

    private void Update()
    {
        if(m_CurrentTimeBeforeSpawn <= m_TimeBeforeSpawn)
        {
            m_CurrentTimeBeforeSpawn += Time.deltaTime;
        }
        else
        {
            m_CurrentTimeBeforeSpawn = 0;

            if(m_NbEnemySpawned < m_NbEnemyToSpawn)
            {
                Instantiate(m_EnemyToInstantiate, transform.position, Quaternion.identity);
                m_NbEnemySpawned += 1;
            }
            
        }
    }
}
