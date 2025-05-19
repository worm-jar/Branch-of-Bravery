using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCam : MonoBehaviour
{
    public GameObject _player;
    public Transform _cam;
    public Transform _cam0;
    public Canvas _moon;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _cam = _player.transform.Find("Camera");
        _moon = this.gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        _moon.worldCamera = _cam.gameObject.GetComponent<Camera>();
    }
}
