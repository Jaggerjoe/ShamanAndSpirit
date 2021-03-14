using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private InputAction m_Move;

    [SerializeField]
    private float m_Speed = 5;

    private PlayerInput m_PlayerInput = null;

    Vector2 move;

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerInput == null)
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_Move = m_PlayerInput.actions["Move"];
        }
        move = m_Move.ReadValue<Vector2>();
        Movement(move, Time.deltaTime);
    }

    private void Movement(Vector2 p_Movement, float p_DeltaTime)
    {
        transform.position += new Vector3(p_Movement.x, 0, p_Movement.y) * m_Speed * p_DeltaTime;
    }
}
