using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderOrientation : MonoBehaviour {

    [SerializeField]
    GameObject leftShoulder, rightShoulder, chest;
	
	// Update is called once per frame
	void Update () {
        leftShoulder.transform.LookAt(rightShoulder.transform.position);
        chest.transform.rotation = leftShoulder.transform.rotation;
    }
}
