using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackAttack : MonoBehaviour
{
    [SerializeField]
    private float m_TimebeforeDestroy;

    private float m_CurrentTimeBeforeDestroy;
    // Update is called once per frame
    void Update()
    {
        
        if(m_CurrentTimeBeforeDestroy < m_TimebeforeDestroy)
        {
            m_CurrentTimeBeforeDestroy += Time.deltaTime;
        
        }
        else
        {
            Destroy(this.gameObject);
            m_CurrentTimeBeforeDestroy = 0;
        }
    }
}
