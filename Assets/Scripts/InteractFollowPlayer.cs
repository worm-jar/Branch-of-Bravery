using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject _player = GameObject.Find("Player");
        transform.position = new Vector2 (_player.transform.position.x, _player.transform.position.y + 2.3f);
    }
}
