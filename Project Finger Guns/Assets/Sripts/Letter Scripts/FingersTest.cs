using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity.Attributes;
using Leap.Unity;
using UnityEngine.UI;

public class FingersTest : MonoBehaviour
{

    /*
      Does not include these letter due to the lack of tracking of data, letters not coded include:H,J,K,R,S,T,W.

     */
    
    Controller controller;
    LeapProvider provider;
    //Vector PalmPosition;
    //Vector PalmDirection;
    //Vector PalmVelocity;
    //Vector3 Distance;
    //Transform Palmpos;

    public Text GestureText;
    public GameObject RightModel;
    public GameObject LeftModel;


    bool rightLetterG = false;
    bool IsExtended;
    public bool letterC;
    bool letterCLeft;
    public bool letterA;
    bool letterZ = false;
    bool letterF = false;
    bool letterB;
    bool letterD;
    bool letterE;
    bool letterL;
    bool letterY;
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
        //PalmPosition = hands[0].PalmPosition;
        //PalmDirection = hands[0].Direction;
        //PalmVelocity = hands[0].PalmVelocity;
        RPinch = hands[0].PinchStrength;
        LPinch = hands[1].PinchStrength;
        RightAngleGrab = hands[0].GrabAngle;
        LeftAngleGrab = hands[1].GrabAngle;

        // print("position" + PalmPosition);
        // print("velocity" + PalmVelocity);
        // print("direction" + PalmDirection);


        Frame frames = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
            //checks for right hand
            if (hand.IsRight)
            {

                //assigns right hand a frame value
                Hand handright = frames.Hands[0];

                //letter A & letter Y
                if (handright.Fingers[1].IsExtended && handright.Fingers[2].IsExtended == false && handright.Fingers[3].IsExtended == false && handright.Fingers[4].IsExtended == false && handright.Fingers[0].IsExtended == false)
                {
                    letterA = true;
                    letterL = true;
                    letterY = true;
                }
                else
                {
                    letterA = false;
                    letterL = false;
                    letterY = false;
                }

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
                    if (RPinch >= 0.3f && RPinch <= 0.8)
                    {
                        letterC = true;
                    }
                    else letterC = false;
                }
               

                //letter D
                if (handright.Fingers[1].IsExtended && handright.Fingers[2].IsExtended && handright.Fingers[3].IsExtended == false && handright.Fingers[2].IsExtended == false && handright.Fingers[4].IsExtended == false)
                {
                    letterD = true;
                }
                else letterD = false;

                //Letter E && I
                if (handright.Fingers[1].IsExtended && handright.Fingers[2].IsExtended == false && handright.Fingers[3].IsExtended == false && handright.Fingers[4].IsExtended == false && handright.Fingers[0].IsExtended == false)
                {
                    letterE = true;
                }
                else letterE = false;

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
                
                //letter M (not 100% will work)

                // letter N (not 100% will work)

                //Letter O 

                //Letter P

                //Letter Q

                //Letter U (Not 100% will work)

                //Letter X (not 100% will work)

                //Letter Y covered in A condition ^
               


                //letterZ
                if (RightAngleGrab >= 1.5 && RightAngleGrab <= 3.1)
                {
                    letterZ = true;
                }
                else letterZ = false;
            }








        foreach (Hand hand in frame.Hands)
            //checks for left hand
            if (hand.IsLeft)
            {
                //assigns left hand a frame value
                Hand handleft = frames.Hands[1];

                //letter A
                if (letterA == true && handleft.Fingers[1].IsExtended && handleft.Fingers[2].IsExtended && handleft.Fingers[3].IsExtended && handleft.Fingers[4].IsExtended && handleft.Fingers[0].IsExtended)
                {
                    if (LeftThumb.LeftThumbTrigger == true)
                    {
                        print("A is true");
                        GestureText.text = "letter A";
                    }

                }
                else LeftThumb.LeftThumbTrigger = false;

                // letter b
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

                //letterC left hand
                if (letterC == true && handleft.Fingers[1].IsExtended == false && handleft.Fingers[2].IsExtended == false && handleft.Fingers[3].IsExtended == false && handleft.Fingers[4].IsExtended == false && handleft.Fingers[0].IsExtended == false)
                {
                    letterCLeft = true;
                    print("Letter C");
                }

                //Letter D parameters for left hand
                //bugged wont register triggers
                if (letterD == true && handleft.Fingers[1].IsExtended && handleft.Fingers[2].IsExtended == false && handleft.Fingers[3].IsExtended == false && handleft.Fingers[4].IsExtended == false && handleft.Fingers[0].IsExtended == false)
                {
                    if (LeftIndexKnuckle.IndexKnuckleTrigger == true && LeftIndex.LeftIndexTriggerLIndex == true)
                    {
                        GestureText.text = "letter D";
                        print("Letter D");
                    }
                }

                //letter E
                if (letterE == true && handleft.Fingers[1].IsExtended && handleft.Fingers[2].IsExtended == false && handleft.Fingers[3].IsExtended == false && handleft.Fingers[4].IsExtended == false && handleft.Fingers[0].IsExtended == false)
                {
                    if (LeftIndex.LeftIndexTriggerLIndex == true && PinkyMiddle.PinkyMiddleTrigger == true)
                    {
                            print("Letter E is correct");
                    }
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
                            GestureText.text = "letter F";
                        }

                    }
                    else LeftIndex.LeftIndexTrigger = false;
                }

                //letter G
                //is not finished
                if (LeftAngleGrab > 3 && rightLetterG == true)
                {
                    GestureText.text = "letter G";
                }

                //letter I & letter O & Letter U
                //ABIT BUGGY I WONT GO FALSE AFTER COLLISION STOPS
                if (handleft.Fingers[1].IsExtended && handleft.Fingers[2].IsExtended && handleft.Fingers[3].IsExtended && handleft.Fingers[4].IsExtended && handleft.Fingers[0].IsExtended)
                {
                    if (letterE == true && LeftMiddleTip.LeftMiddleTipTrigger == true)
                    {
                        print("Letter I");
                    }
                    else LeftMiddleTip.LeftMiddleTipTrigger = false;

                    //letter O
                    if (letterE == true && RingTip.LeftRingTip == true)
                    {
                        print("letter O");
                    }
                    else RingTip.LeftRingTip = false;

                    if (letterE == true && PinkyTip.LeftPinkyTip == true)
                    {
                        print("Letter U");
                    }
                    else PinkyTip.LeftPinkyTip = false;
                }
                
                
                //letter M done

                // letter N done

                //Letter O done

                //Letter P

                //Letter Q

                //Letter U done

                //Letter X (not 100% will work)
                if(handleft.Fingers[1].IsExtended && handleft.Fingers[2].IsExtended == false && handleft.Fingers[3].IsExtended == false && handleft.Fingers[4].IsExtended == false && handleft.Fingers[0].IsExtended == false)
                {

                }

                //Letter Y && Letter L
                if(handleft.Fingers[1].IsExtended && handleft.Fingers[2].IsExtended && handleft.Fingers[3].IsExtended && handleft.Fingers[4].IsExtended && handleft.Fingers[0].IsExtended)
                {
                    //letter L
                    if (BasePalm.LeftHandBaseL == true)
                    {
                        if (letterL == true)
                        {
                            print("Letter L is True");
                        }
                    }
                    else BasePalm.LeftHandBaseL = false;

                    //letter N
                    if (BasePalm.LeftHandBaseN == true)
                    {
                        if (letterL == true)
                        {
                            print("letter N is true");
                        }
                    }
                    else BasePalm.LeftHandBaseN = false;

                    //letter M
                    if (BasePalm.LeftHandBaseM == true)
                    {
                        if (letterL == true)
                        {
                            print("letter M is true");
                        }
                    }
                    else BasePalm.LeftHandBaseM = false;

                    //letter Y
                    if (BaseThumb.LeftYThumbTrigger == true)
                    {
                        if (letterY == true)
                        {
                            print("letter Y is True");
                        }
                    }
                }
                else BaseThumb.LeftYThumbTrigger = false;

                // letter Z
                if (LeftAngleGrab == 0 && letterZ == true)
                {
                    print("Z gesture");
                    GestureText.text = "letter Z";
                }
            }
    }

}


