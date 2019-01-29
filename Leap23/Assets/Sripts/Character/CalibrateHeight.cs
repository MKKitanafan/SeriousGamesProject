using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class CalibrateHeight : MonoBehaviour {

    [SerializeField]
    GameObject leftAnkle, rightAnkle, head, kLeftFoot, kRightFoot, kHead;

    float timeLeft = 3f;
    bool countdown = false;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            countdown = true;
            print("Stand Straight");
        }

        if (countdown==true)
        {
            timeLeft -= Time.deltaTime;
            
            if (timeLeft < 0)
            {
                countdown = false;
                timeLeft = 3f;
                Calibrate();
            }
        }
    }

    void Calibrate()
    {
        gameObject.GetComponent<VRIK>().enabled = false;
        transform.localScale = new Vector3(1,1,1);
        float resize = 0;
        float modelHeight, playerHeight;
        Vector3 modelBase, kinectBase;

        modelBase.x = leftAnkle.transform.position.x + (rightAnkle.transform.position.x - leftAnkle.transform.position.x) / 2;
        modelBase.y = leftAnkle.transform.position.y + (rightAnkle.transform.position.y - leftAnkle.transform.position.y) / 2;
        modelBase.z = leftAnkle.transform.position.z + (rightAnkle.transform.position.z - leftAnkle.transform.position.z) / 2;

        kinectBase.x = kLeftFoot.transform.position.x + (kRightFoot.transform.position.x - kLeftFoot.transform.position.x) / 2;
        kinectBase.y = kLeftFoot.transform.position.y + (kRightFoot.transform.position.y - kLeftFoot.transform.position.y) / 2;
        kinectBase.z = kLeftFoot.transform.position.z + (kRightFoot.transform.position.z - kLeftFoot.transform.position.z) / 2;

        modelHeight = Vector3.Distance(modelBase, head.transform.position);
        playerHeight = Vector3.Distance(kinectBase, kHead.transform.position);

        resize = playerHeight / modelHeight;
        print(resize);
        //transform.localScale = new Vector3(resize, resize, resize);
        gameObject.GetComponent<VRIK>().enabled = true;
        return;
    }
}
