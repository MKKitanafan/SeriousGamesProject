using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GestureTest : MonoBehaviour {

    public Text MyText;
    public Text Pitch;
    public Text Yaw;
    public Text Roll;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RockOnPrint()
    {
        MyText.text = "Rock ON!";
      print("Active rock");
    }

    public void RockOffPrint()
    {
        MyText.text = "Party Pooper!";
        print("De-Active rock");
    }

    public void GunOnPrint()
    {
        MyText.text = "Hands UP Punk!";
        print("Gun active");
    }
    public void GunOffPrint()
    {
        MyText.text = "Just Kidding";
        print("Gun de-active");
    }
}
