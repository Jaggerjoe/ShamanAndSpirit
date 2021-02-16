using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private GameObject m_TargetToFollow = null;

    [SerializeField]
    private Vector3 m_Offset;
    // Start is called before the first frame update
    void Start()
    {
        m_Offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_TargetToFollow.transform.position + m_Offset;
    }
}
