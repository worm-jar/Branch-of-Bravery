using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public static bool hasCheckpoint;
    public static bool touchingInteract;
    public static string interactName;
    // Start is called before the first frame update
    void Start()
    {
        hasCheckpoint = false;
        touchingInteract = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            touchingInteract = true;
            interactName = collision.gameObject.name;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            touchingInteract = true;
            interactName = collision.gameObject.name;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            touchingInteract = false;
        }
    }
}
