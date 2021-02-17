using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    [SerializeField]
    private List<Transform> m_TargetList = new List<Transform>();

    [SerializeField]
    private Vector3 m_Ofsset;
    private float m_SmoothTime = .5f;
    private Vector3 velocity;

    [SerializeField]
    private float m_MinZoom = 40f;
    [SerializeField]
    private float m_MaxZoom = 10f;

    [SerializeField]
    private float m_ZoomLimiter = 50f;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    
    private void LateUpdate()
    {
        if (m_TargetList.Count == 0)
        {
            return;
        }
        Move();
        Zoom();
    }

    public void Zoom()
    {
        float newZoom = Mathf.Lerp(m_MaxZoom, m_MinZoom, GetGreaterDistane() / m_ZoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }
    public void Move()
    {
        Vector3 center = GetCenterPoint();

        Vector3 newPosition = center + m_Ofsset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, m_SmoothTime);
        transform.LookAt(center);
    }
    float GetGreaterDistane()
    {
        var bounds = new Bounds(m_TargetList[0].position, Vector3.zero);
        for (int i = 0; i < m_TargetList.Count; i++)
        {
            bounds.Encapsulate(m_TargetList[i].position);
        }
        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if(m_TargetList.Count ==1)
        {
            return m_TargetList[0].position;
        }
        var bounds = new Bounds(m_TargetList[0].position, Vector3.zero);
        for (int i = 0; i < m_TargetList.Count; i++)
        {
            bounds.Encapsulate(m_TargetList[i].position);
        }
        return bounds.center;
    }
}
