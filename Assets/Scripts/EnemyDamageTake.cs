using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTake : MonoBehaviour
{
    public float knockBack;
    public Rigidbody2D _rig;
    public GameObject _player;
    public float timerIFrames;
    // Start is called before the first frame update
    void Start()
    {
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIFrames > 0)
        {
            timerIFrames -= Time.deltaTime;
            if (timerIFrames <= 0)
            {
                this.gameObject.layer = LayerMask.NameToLayer("Enemy");
                timerIFrames = 0;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Attack"))
        {
            if (PlayerAttack.isLightAttacking)
            {
                PlayerHealth.health += 3;
                EnemyHealth.health -= 2.5f;
            }
            else if (PlayerAttack.isStrongAttacking)
            {
                PlayerHealth.health -= 20;
                EnemyHealth.health -= 12;
                PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 50, 100);
            }
                this.gameObject.layer = LayerMask.NameToLayer("Invincible");
            float relativePos = transform.position.x - _player.transform.position.x;
            _rig.AddForce(new Vector2(relativePos * knockBack, 2f), ForceMode2D.Impulse);
            _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -3.5f, 3.5f), _rig.velocity.y);
            timerIFrames = 0.5f;
        }
    }
}
