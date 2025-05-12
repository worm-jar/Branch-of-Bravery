using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHP : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider _slider;
    public GameObject _player;
    public GameObject _door;
    public Transform _cam;
    public Camera _cam2;
    public Transform _canvas;
    public Canvas _canvas2;
    void Awake()
    {
        _door = GameObject.Find("Door");
        _canvas = _door.transform.Find("Canvas");
        _canvas2 = _canvas.GetComponent<Canvas>();
        _player = GameObject.Find("Player");
        _cam = _player.transform.Find("Camera");
        _cam2 = _cam.GetComponent<Camera>();
        _slider = GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
        _canvas2.worldCamera = _cam2;
        transform.position = new Vector2(_door.transform.position.x + 0.2f, _door.transform.position.y);
        _slider.value = DoorDestroy.health;
    }
}
