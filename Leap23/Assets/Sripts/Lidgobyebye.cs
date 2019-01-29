using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lidgobyebye : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "key")
        {
            Destroy(gameObject, 0f);
        }
    }
}
