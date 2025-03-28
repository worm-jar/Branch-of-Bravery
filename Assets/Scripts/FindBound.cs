using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindBound : MonoBehaviour
{
    public PolygonCollider2D closeToHome;
    public CinemachineConfiner2D confiner;
    // Start is called before the first frame update
    void Start()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        closeToHome = GameObject.Find("Bound").GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        confiner.m_BoundingShape2D = closeToHome;
    }
}
