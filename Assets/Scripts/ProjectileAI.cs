using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAI : MonoBehaviour
{
    public Rigidbody2D _rig;
    public float speed;
    public float setDir;
    // Start is called before the first frame update
    void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        if (EnemyAI.storeNormDir < 0 )
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (EnemyAI.storeNormDir > 0 )
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        setDir = EnemyAI.storeNormDir;
    }

    // Update is called once per frame
    void Update()
    {
        _rig.velocity = new Vector2(speed * setDir, 0f);
        Destroy(this.gameObject, 10f);
    }
}
