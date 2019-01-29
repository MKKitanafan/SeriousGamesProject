using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine.UI;

public class Locomotion : MonoBehaviour
{

    Controller controller;
    LeapProvider provider;


    float HandPalmPitch;
    float HandPalmYaw;
    float HandPalmRoll;
    float HandWristRot;

    bool HandCheckL = false;
    bool RotationCheck = false;
    bool movementCheck = false;


    public Text Pitch;
    public Text Yaw;
    public Text Roll;

    // Use this for initialization
    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    // Update is called once per frame
    void Update()
    {
        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;
        if (frame.Hands.Count > 0)
        {
            Hand fristHand = hands[0];
        }

        HandPalmPitch = hands[0].PalmNormal.Pitch;
        HandPalmRoll = hands[0].PalmNormal.Roll;
        HandPalmYaw = hands[0].PalmNormal.Yaw;
        HandWristRot = hands[0].WristPosition.Pitch;

        print("Pitch =" + HandPalmPitch);
        Pitch.text = "Pitch =" + HandPalmPitch;
        print("Roll =" + HandPalmRoll);
        Roll.text = "Roll = " + HandPalmRoll;
        print("Yaw =" + HandPalmYaw);
        Yaw.text = "Yaw =" + HandPalmYaw;

        //move Object
        Frame frames = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft)
            {
                HandCheckL = true;
                Debug.Log("Left hand is present");

                if (HandCheckL == true && RotationCheck == false)
                {

                    if (HandPalmRoll < 2 && HandPalmPitch > -2f && HandPalmPitch < 3.5f)
                    {
                        transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime / 4));
                        movementCheck = true;
                    }
                    else if (HandPalmRoll > -2 && HandPalmPitch < -2.2f)
                    {
                        transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime / 4));
                        movementCheck = true;
                    }
                    else movementCheck = false;


                }

                if (HandCheckL == true && movementCheck == false)
                {
                    if (HandPalmYaw > -1.3f && HandPalmRoll > -2.7f && HandPalmRoll < -1.5f)
                    {
                        transform.RotateAround(Vector3.zero, Vector3.up, 40 * Time.deltaTime);
                        RotationCheck = true;
                    }
                    else if (HandPalmYaw < -2.2f && HandPalmRoll < 0.4f && HandPalmRoll > -0.3f)
                    {
                        transform.RotateAround(Vector3.zero, Vector3.down, 40 * Time.deltaTime);
                        RotationCheck = true;
                    }
                    else RotationCheck = false;
                }
            }
        }


    }
}
