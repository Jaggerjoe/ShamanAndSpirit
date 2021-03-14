using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private PlayerInput m_SpellsInput = null;
    private InputAction m_ChangeScene = null;
    private Vector3 m_SceneChangeDirection = Vector3.zero;
    private int m_CurrentScene = 0;
    private int m_MaxScenes = 2;


    void Update()
    {
        if (m_SpellsInput == null)
        {
            m_SpellsInput = GetComponent<PlayerInput>();
            m_ChangeScene = m_SpellsInput.actions["SceneChanger"];
        }
        else
        {
            if (m_ChangeScene.ReadValue<Vector2>().x < 0.0f)
            {
                m_CurrentScene = m_CurrentScene - 1;
                if (m_CurrentScene < 0)
                {
                    m_CurrentScene = m_MaxScenes - 1;
                }
                SceneManager.LoadScene(m_CurrentScene);
            }
            else if (m_ChangeScene.ReadValue<Vector2>().x < 0.0f)
            {
                m_CurrentScene = m_CurrentScene + 1;
                if (m_CurrentScene == m_MaxScenes)
                {
                    m_CurrentScene = 0;
                }
                SceneManager.LoadScene(m_CurrentScene);
            }
        }
    }
}
