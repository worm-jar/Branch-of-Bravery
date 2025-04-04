using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DoorShake : MonoBehaviour
{
    public float speed;
    public float shakeTimer;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player Attack") && PlayerAttack.isStrongAttacking)
        {
            speed = 75f;
            shakeTimer = 0.3f;
            StartCoroutine(Shake());
        }
        if (other.gameObject.CompareTag("Player Attack") && PlayerAttack.isLightAttacking)
        {
            speed = 120f;
            shakeTimer = 0.1f;
            StartCoroutine(Shake());
        }
    }
    IEnumerator Shake()
    {
        while (shakeTimer > 0)
        {
            transform.position = new Vector3(transform.position.x + Mathf.Sin(Time.time*speed)/200, transform.position.y + Mathf.Sin(Time.time*speed)/300, transform.position.z);
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
            shakeTimer = 0;
            }
           yield return null;
        }
        this.transform.position = new Vector2(29.27f, 1.6f);
    }
}
