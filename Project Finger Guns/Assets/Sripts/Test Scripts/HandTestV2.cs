using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTestV2 : MonoBehaviour {

    public GameObject tip, thumb, wrist;

	// Update is called once per frame
	void Update () {

        // x and y need to look at tip
        // z needs to look at thumb

        wrist.transform.LookAt(tip.transform.position);
        Vector3 targetDir = wrist.transform.position - thumb.transform.position;
        float angle = Vector3.Angle(targetDir, transform.up);
        print(angle);
        wrist.transform.RotateAround(transform.position, transform.up, Time.deltaTime * angle);
    }
}
