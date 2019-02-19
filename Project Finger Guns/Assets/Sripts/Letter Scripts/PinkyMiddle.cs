using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyMiddle : MonoBehaviour
{

    public static bool PinkyMiddleTrigger;

    void OnTriggerEnter(Collider cols)
    {
        //letter E
        if (cols.transform.tag == "RThumbMiddle")
        {
            PinkyMiddleTrigger = true;
        }
    }
}
