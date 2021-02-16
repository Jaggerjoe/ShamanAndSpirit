using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private SO_PlaterController m_InputController = null;

    [SerializeField]
    private float m_Speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement(m_InputController.Movement, Time.deltaTime);
    }

    private void Movement(Vector2 p_Movement, float p_DeltaTime)
    {
        transform.position += new Vector3(p_Movement.x, 0, p_Movement.y) * m_Speed * p_DeltaTime;
    }
}
