using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHP : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider _slider;
    public GameObject _door;
    void Start()
    {
        _door = GameObject.Find("Door");
        _slider = GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(_door.transform.position.x + 0.2f, _door.transform.position.y);
        _slider.value = DoorDestroy.health;
    }
}
