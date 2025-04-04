using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHills : MonoBehaviour
{
    public GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Mathf.Clamp((_player.transform.position.x / 2f) - 6.75f, -13f, 5f), (_player.transform.position.y / 25)-1.5f, 2f);
    }
}
