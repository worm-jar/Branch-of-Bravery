using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindBound : MonoBehaviour
{
    public GameObject bound;
    public PolygonCollider2D polyCollider;
    public CinemachineConfiner2D confiner;
    // Start is called before the first frame update
    void Start()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bound = GameObject.Find("Bound");
        polyCollider = bound.GetComponent<PolygonCollider2D>();
        confiner.m_BoundingShape2D = polyCollider;
    }
}
