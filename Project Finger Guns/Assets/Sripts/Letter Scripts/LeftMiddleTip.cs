using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMiddleTip : MonoBehaviour
{
    public static bool LeftMiddleTipTrigger;

    void OnTriggerEnter(Collider cols)
    {

        if (cols.transform.tag == "RIndexTip")
        {
            LeftMiddleTipTrigger = true;
        }
    }
}
