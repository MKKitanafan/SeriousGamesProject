using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftIndex : MonoBehaviour {

    public static bool LeftIndexTrigger;
    public static bool LeftIndexTriggerLIndex;
    

    void OnTriggerEnter(Collider cols)
    {
        
        if (cols.transform.tag == "RMiddleTip")
        {
            LeftIndexTrigger = true;
        }

        if (cols.transform.tag == "RIndexTip")
        {
            LeftIndexTriggerLIndex = true;
        }
       
    }

 
}
