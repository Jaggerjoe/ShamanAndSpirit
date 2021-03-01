using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody m_RigidBody;
    private PlayerManager m_Player;
   
    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Player= FindObjectOfType<PlayerManager>();
        
    }
    private void Start()
    {

        m_RigidBody.AddForce((m_Player.transform.position), ForceMode.Impulse);
    }
}
