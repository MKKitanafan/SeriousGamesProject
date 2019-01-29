using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class DetectGrabLeft : MonoBehaviour {

    public GameObject BodySourceManager;
    private BodySourceManager _BodyManager;
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();

    private Animator bodyAnim;

    private bool leftGrab, leftGrabOld;
    public float countdownLength = 1f;
    float countdownLeft, second;
    public float grabDistance = 0.07f;

    private void Start()
    {
        bodyAnim = gameObject.GetComponent<Animator>();
        countdownLeft = countdownLength;
        second = 0f;
    }

    void Update()
    {
        if (BodySourceManager == null)
        {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                HandInFist(Kinect.JointType.ThumbLeft, body);
            }
        }
    }

    private bool GrabChange(bool oldGrab, bool newGrab, float timer)
    {
        second += Time.deltaTime;
        if (newGrab != oldGrab)
        {
            timer -= Time.deltaTime;
        }
        if (second > countdownLength)
        {
            if (timer > (countdownLength * 0.8))
            {
                oldGrab = newGrab;
            }
            second = 0f;
            timer = countdownLength;
        }
        return oldGrab;
    }
    
    private void HandInFist(Kinect.JointType jt, Kinect.Body body)
    {
        Kinect.JointType jt2;
        jt2 = Kinect.JointType.HandTipLeft;
        if (Vector3.Distance(GetVector3FromJoint(body.Joints[jt]), GetVector3FromJoint(body.Joints[jt2])) < grabDistance)
        {
            leftGrab = true;
        }
        else
        {
            leftGrab = false;
        }
        leftGrabOld = GrabChange(leftGrabOld, leftGrab, countdownLeft);
        bodyAnim.SetBool("LeftFist", leftGrabOld);
    }


    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        //Node positioning, can be used to set scale
        return new Vector3(joint.Position.X, joint.Position.Y, -joint.Position.Z);
    }
}
