using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroy : MonoBehaviour
{
    public int health = 4;
    // Start is called before the first frame update
    void Start()
    {
        health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            //playParticles
            Destroy(this.gameObject);
            DoorHasBeenDestroyed.destroyed = true;
        }
        if (DoorHasBeenDestroyed.destroyed)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player Attack") && PlayerAttack.isStrongAttacking)
        {
            health -= 1;
        }
        if (other.gameObject.CompareTag("Player Attack") && PlayerAttack.isLightAttacking)
        {
            PlayerHealth.health += 8f;
            PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 0, 100);
        }
    }
}
