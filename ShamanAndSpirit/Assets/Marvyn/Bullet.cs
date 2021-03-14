using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody m_RigidBody;
    private PlayerMovement m_Player;
   
    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        PlayerMovement[] m_Players = FindObjectsOfType<PlayerMovement>();
        foreach (PlayerMovement l_Player in m_Players)
        {
            if (l_Player.tag == "Shaman")
            {
                m_Player = l_Player;
            }
        }
        
    }
    private void Start()
    {
        m_RigidBody.AddForce((m_Player.transform.position - transform.position), ForceMode.Impulse);
    }
}
