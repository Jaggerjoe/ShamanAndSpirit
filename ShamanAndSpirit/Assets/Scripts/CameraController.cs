using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_TargetShamanToFollow = null;
    [SerializeField]
    private GameObject m_TargetSpirit = null;
    [SerializeField]
    private Vector3 m_Offset;

    private Vector3 m_EditorPoint;
    // Start is called before the first frame update
    void Start()
    {
        m_Offset = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetPointBetweenTwoEntity();
    }
    void GetPointBetweenTwoEntity()
    {
        Vector3 l_TargetPoint = (m_TargetShamanToFollow.transform.position + m_TargetSpirit.transform.position)/2;
        transform.position = l_TargetPoint + m_Offset;
        m_EditorPoint = l_TargetPoint;
        float l_Dist = Vector3.Distance(m_TargetShamanToFollow.transform.position, m_TargetSpirit.transform.position);
        if(l_Dist >= 10)
        {
            m_Offset.z += 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_EditorPoint, 1f);
    }
}

