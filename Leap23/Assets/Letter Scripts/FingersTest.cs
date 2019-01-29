using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity.Attributes;
using Leap.Unity;
using UnityEngine.UI;

public class FingersTest : MonoBehaviour
{


    Controller controller;
    LeapProvider provider;
    Vector PalmPosition;
    Vector PalmDirection;
    Vector PalmVelocity;
    Vector3 Distance;
    Transform Palmpos;

    public Text GestureText;
    public GameObject RightModel;
    public GameObject LeftModel;
    public GameObject leftKHand, rightKHand;
    public AudioClip ding;
    AudioSource audioSource;
    bool dingBool;

    bool rightLetterG = false;
    bool IsExtended;
    public bool letterC;
    public bool letterA;
    bool letterZ = false;
    bool letterF = false;
    bool letterB;
   public bool FKey = false;
    float RPinch;
    float LPinch;
    float RightAngleGrab;
    float LeftAngleGrab;

    
    void Start()
    {
        //Gets frame data
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        controller = new Controller();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        // controller is a Controller object
        Frame frame = controller.Frame(); 

        List<Hand> hands = frame.Hands;
        if (frame.Hands.Count > 0)
        {
            Hand firstHand = hands[0];
        }
        PalmPosition = hands[0].PalmPosition;
        PalmDirection = hands[0].Direction;
        PalmVelocity = hands[0].PalmVelocity;
        RPinch = hands[0].PinchStrength;
        LPinch = hands[1].PinchStrength;
        RightAngleGrab = hands[0].GrabAngle;
        LeftAngleGrab = hands[1].GrabAngle;


        // print("position" + PalmPosition);
        // print("velocity" + PalmVelocity);
        // print("direction" + PalmDirection);

        

       
        Frame frames = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {


            if (frame.Hands.Count < 2)
            {
                if (hand.IsLeft)
                {
                    rightKHand.SetActive(true);
                    leftKHand.SetActive(false);
                }
                else if (hand.IsRight)
                {
                    leftKHand.SetActive(true);
                    rightKHand.SetActive(false);
                }
                else
                {
                    rightKHand.SetActive(false);
                    leftKHand.SetActive(false);
                }
            }
            //checks for right hand
            if (hand.IsRight)
            {

                //assigns right hand a frame value
                Hand handright = frames.Hands[0];

                //letter B
                if (handright.Fingers[3].IsExtended == true && handright.Fingers[2].IsExtended == true && handright.Fingers[4].IsExtended == true)
                {
                    if (hand.IsRight && RPinch >= 1.0f)
                    {
                        letterB = true;
                    }
                    else letterB = false;
                }


                //Letter C
                if (handright.Fingers[3].IsExtended == false && handright.Fingers[2].IsExtended == false && handright.Fingers[4].IsExtended == false)
                {
                    if (hand.IsRight && RPinch >= 0.3f && RPinch <= 0.8)
                    {
                        letterC = true;
                        print("Letter C");
                    }
                }
                else letterC = false;

                //letterZ
                if (RightAngleGrab >= 1.5 && RightAngleGrab <= 3.1)
                {
                    letterZ = true;
                }
                else letterZ = false;

                //Letter F
                if (handright.Fingers[1].IsExtended && handright.Fingers[2].IsExtended && handright.Fingers[3].IsExtended == false && handright.Fingers[4].IsExtended == false && handright.Fingers[0].IsExtended == false)
                {
                    letterF = true;
                }
                else letterF = false;

                //Letter G
                if (RightAngleGrab > 3)
                {
                    rightLetterG = true;
                }
                else rightLetterG = false;

                //letter A
                if (handright.Fingers[1].IsExtended && handright.Fingers[2].IsExtended == false && handright.Fingers[3].IsExtended == false && handright.Fingers[4].IsExtended == false && handright.Fingers[0].IsExtended == false)
                {
                    letterA = true;
                }
                else letterA = false;

            }
        }
       foreach (Hand hand in frame.Hands)
            //checks for left hand
            if (hand.IsLeft)
            {
                //assigns left hand a frame value
                Hand handleft = frames.Hands[1];

                //letter A
                if(letterA == true && handleft.Fingers[1].IsExtended && handleft.Fingers[2].IsExtended && handleft.Fingers[3].IsExtended && handleft.Fingers[4].IsExtended && handleft.Fingers[0].IsExtended)
                {
                    if (LeftThumb.LeftThumbTrigger == true)
                    {
                        print("A is true");
                        GestureText.text = "letter A";
                    }
                            
                }
                    else LeftThumb.LeftThumbTrigger = false;

                if (handleft.Fingers[3].IsExtended == true && handleft.Fingers[2].IsExtended == true && handleft.Fingers[4].IsExtended == true)
                {
                    if (hand.IsLeft && LPinch >= 1.0f)
                    {
                        if (LeftThumb.LeftBThumbTrigger == true && LeftIndex.LeftIndexTriggerLIndex == true)
                        {
                            if (letterB == true)
                            {
                                print("Letter B");
                            }
                            else letterB = false;
                        }
                    }
                    
                }

                //Letter D parameters for left hand
                if (letterC == true && handleft.Fingers[1].IsExtended && handleft.Fingers[2].IsExtended == false && handleft.Fingers[3].IsExtended == false && handleft.Fingers[4].IsExtended == false && handleft.Fingers[0].IsExtended == false)
                {
                    if(LeftIndexKnuckle.IndexKnuckleTrigger == true && LeftIndex.LeftIndexTriggerLIndex == true)
                    {
                          GestureText.text = "letter D";
                          print("Letter D");
                    }
                       
                }

                if (LeftAngleGrab == 0 && letterZ == true)
                {
                        print("Z gesture");
                        GestureText.text = "letter Z";
                }

                // Letter F parameters 
                if (handleft.Fingers[1].IsExtended && handleft.Fingers[2].IsExtended && handleft.Fingers[3].IsExtended == false && handleft.Fingers[4].IsExtended == false && handleft.Fingers[0].IsExtended == false)
                {
                    if (letterF == true)
                    {
                        if (LeftIndex.LeftIndexTrigger == true)
                        {
                            print("Letter F");
                            FKey = true;
                            AudioDing();
                            GestureText.text = "letter F";
                        }

                    }
                    else LeftIndex.LeftIndexTrigger = false;


                }
                if (LeftAngleGrab > 3  && rightLetterG == true)
                {
                    GestureText.text = "letter G";
                }
                
            }  
    }

    public void AudioDing()
    {
        if (!dingBool)
        {
            audioSource.PlayOneShot(ding, 0.7F);
            dingBool = true;
        }
    }








    //list finger attributes inside inspector

    // public List<Finger> Fingers;
    //public Finger Finger(int id)
    //{
    //    for (int i = Fingers.Count; i-- != 0;)
    //    {
    //        if (Fingers[i].Id == id)
    //        {
    //            return Fingers[i];
    //        }
    //    }
    //    return null;
    //}

}


  