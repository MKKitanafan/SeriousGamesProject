using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePalm : MonoBehaviour {

    public static bool LeftHandBaseL;
    public static bool LeftHandBaseM;
    public static bool LeftHandBaseN;

    void OnTriggerEnter(Collider cols)
    {
        //letter L
        if (cols.transform.tag == "RIndexTip")
        {
            LeftHandBaseL = true;
        }
        


        //letter N
        if (cols.transform.tag == "RIndexTip" && cols.transform.tag == "RMiddleTip")
        {
            LeftHandBaseN = true;
        }
        

        //letter M
        if (cols.transform.tag == "RIndexTip" && cols.transform.tag == "RMiddleTip" && cols.transform.tag == "RRingTip")
        {
            LeftHandBaseM = true;
        }
       
    }
}
