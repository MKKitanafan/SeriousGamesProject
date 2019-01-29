using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour {

    public GameObject head;
    public float offSetY;
    public float offSetZ;
    public float offSetX;
    Quaternion rotation;

    // Update is called once per frame
    void Update () {
        head = GameObject.FindGameObjectWithTag("Head");
        if (head) {
            rotation = Quaternion.Euler(head.transform.rotation.x, head.transform.rotation.y + 180f, head.transform.rotation.z);
            gameObject.transform.position = head.transform.position;
            gameObject.transform.rotation = rotation;
        }
    }
}
