using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New_PlayerController", menuName = "Game")]
public class SO_PlaterController : ScriptableObject
{
    [SerializeField]
    private InputActionAsset m_InputManger = null;

    private Vector2 m_MoveVector = Vector2.zero;
    private void OnEnable()
    {
        BindInputs(true);
    }

    private void OnDisable()
    {
        BindInputs(false);
    }

    private void BindInputs(bool p_AreEnebled)
    {
        if(m_InputManger == null)
        {
            return;
        }
        if(p_AreEnebled)
        {
            m_InputManger.FindAction("player/Move").performed += Move;
            m_InputManger.FindAction("player/Move").canceled += Move;

            m_InputManger.Enable();
        }
        else
        {
            m_InputManger.FindAction("player/Move").performed -= Move;
            m_InputManger.FindAction("player/Move").canceled -= Move;

            m_InputManger.Disable();
        }
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        m_MoveVector = ctx.ReadValue<Vector2>();
        m_MoveVector = Vector3.ClampMagnitude(m_MoveVector, 1f);
    }

    public Vector2 Movement => m_MoveVector;
}
