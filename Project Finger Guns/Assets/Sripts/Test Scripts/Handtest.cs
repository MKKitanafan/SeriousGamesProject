using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handtest : MonoBehaviour {

    public GameObject tip, thumb, wrist;

	// Update is called once per frame
	void Update () {

        // x and y need to look at tip
        // z needs to look at thumb

        Vector3 relativePos = thumb.transform.localPosition - transform.localPosition;
        Vector3 relativePos2 = tip.transform.localPosition - transform.localPosition;
        Quaternion rotationY = Quaternion.LookRotation(relativePos, Vector3.up);
        Quaternion rotationXz = Quaternion.LookRotation(relativePos2, Vector3.right);
        wrist.transform.localRotation = Quaternion.RotateTowards(wrist.transform.localRotation, rotationXz, Time.deltaTime * 250f);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotationY, Time.deltaTime * 250f);
    }
}
