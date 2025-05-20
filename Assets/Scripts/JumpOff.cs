using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class JumpOff : MonoBehaviour
{
    public GameObject _player;
    public GameObject JumpMan;
    public Rigidbody2D _rig;
    public float distanceX;
    public float normDistance;
    public JumpAI JumpAI;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        JumpMan = this.gameObject.transform.parent.gameObject;
        JumpAI = JumpMan.GetComponent<JumpAI>();
        _player = GameObject.Find("Player");
        _rig = JumpMan.GetComponent<Rigidbody2D>();
        distanceX = _player.transform.position.x - transform.position.x;
        if (distanceX < 0)
        {
            normDistance = -1f;
        }
        if (distanceX >= 0)
        {
            normDistance = 1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _rig.AddForce(new Vector2(-normDistance * Random.Range(15f, 18), 1f), ForceMode2D.Impulse);
            JumpAI._audioSource.clip = JumpAI._dodge;
            JumpAI._audioSource.Play();
        }
    }
}
