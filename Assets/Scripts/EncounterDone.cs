using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterDone : MonoBehaviour
{
    public static bool doneSet;
    public void Update()
    {
        if(CheckEnemy.done)
        {
            doneSet = true;
        }
    }
}
