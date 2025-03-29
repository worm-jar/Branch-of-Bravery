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
        this.transform.position = new Vector3(Mathf.Clamp((_player.transform.position.x/2f)-5.75f, -10f, 5f), (_player.transform.position.y / 4), 2f);
    }
}
