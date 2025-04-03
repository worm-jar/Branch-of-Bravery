using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DoorShake : MonoBehaviour
{
    public float speed;
    public float shakeTimer;
    public Transform originalTrans;
    // Start is called before the first frame update
    void Start()
    {
        originalTrans = transform;
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player Attack") && PlayerAttack.isStrongAttacking)
        {
            StartCoroutine(Shake());
        }
    }
    IEnumerator Shake()
    {
        shakeTimer = 0.2f;
        while (shakeTimer > 0)
        {
            transform.position = new Vector3(transform.position.x + Mathf.Sin(Time.time*speed)/200, transform.position.y + Mathf.Sin(Time.time*speed)/300, transform.position.z);
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                transform.position = originalTrans.position;
                shakeTimer = 0;
            }
            yield return null;
        }
    }
}
