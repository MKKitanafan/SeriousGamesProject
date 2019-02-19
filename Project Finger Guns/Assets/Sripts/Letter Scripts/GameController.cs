using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    GameObject rightHand;

    FingersTest FTest;

    [SerializeField]
    GameObject item1;

    void Start()
    {
        FTest = rightHand.GetComponent<FingersTest>();
    }

    // Update is called once per frame
    void Update () {
    
        if (FTest.FKey == true)
        {
            item1.SetActive(true);

        }

    }
}
