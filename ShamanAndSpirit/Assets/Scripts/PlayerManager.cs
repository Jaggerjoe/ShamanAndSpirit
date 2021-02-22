using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInputManager m_PlayerInputManager;
    [SerializeField]
    private GameObject player, spirit;


    // Start is called before the first frame update
    void Start()
    {
        var p1 = PlayerInput.Instantiate(player, controlScheme: "KeyBoard");
        var p2 = PlayerInput.Instantiate(spirit, controlScheme: "Controller");
    }
}
