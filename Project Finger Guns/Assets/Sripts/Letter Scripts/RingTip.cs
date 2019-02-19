using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTip : MonoBehaviour {

    public static bool LeftRingTip;

    void OnTriggerEnter(Collider cols)
    {
        //letter O
        if (cols.transform.tag == "RIndexTip")
        {
            LeftRingTip = true;
        }

    }

    }
