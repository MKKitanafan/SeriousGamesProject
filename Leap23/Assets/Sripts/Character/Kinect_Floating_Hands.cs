using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;


[RequireComponent(typeof(Animator))]

public class Kinect_Floating_Hands : MonoBehaviour
{
    protected Animator animator;
    public GameObject BodySourceManager;
    public int players = 1;

    [SerializeField]
    public GameObject head, wristLeft, handTipLeft, wristRight, handTipRight;

    public static bool mapped = false;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.WristLeft, Kinect.JointType.ThumbLeft },
        { Kinect.JointType.WristRight, Kinect.JointType.ThumbRight },
    };

    private Dictionary<Kinect.JointType, GameObject> _Joints;

    private void Start()
    {
        animator = GetComponent<Animator>();

        _Joints = new Dictionary<Kinect.JointType, GameObject>()
        {
            { Kinect.JointType.Head, head },
            { Kinect.JointType.WristLeft, wristLeft },
            { Kinect.JointType.HandTipLeft, handTipLeft },
            { Kinect.JointType.WristRight, wristRight },
            { Kinect.JointType.HandTipRight, handTipRight },
        };
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
                mapped = true;
                trackedIds.Add(body.TrackingId);
            } else
            {
                mapped = false;
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
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
                MoveIKGoal(body);
            }
        }
    }


    private void MoveIKGoal(Kinect.Body body)
    {
        Vector3 offset = GetVector3FromJoint(body.Joints[Kinect.JointType.Head]);
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint joint = body.Joints[jt];

            GameObject temp = null;
            if (_Joints.TryGetValue(jt, out temp))
            {
                GameObject jointPos = _Joints[jt];
                jointPos.transform.localPosition = GetVector3WithOffset(joint, offset); //USE OFFSET?
            }


            // HAND ANGLE AND ROTATION
            if (jt == Kinect.JointType.WristLeft) 
            {
                GameObject wrist = _Joints[jt];
                wrist.transform.LookAt(handTipLeft.transform.position);
               // wrist.transform.LookAt(ThumbLeft.transform.position);
            }

            if (jt == Kinect.JointType.WristRight)
            {
                GameObject wrist = _Joints[jt];
                wrist.transform.LookAt(handTipRight.transform.position);
             //   wrist.transform.LookAt(ThumbRight.transform.position);
            }
        }

    }

    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        //Node positioning, can be used to set scale
        return new Vector3(joint.Position.X, joint.Position.Y, -joint.Position.Z);
    }

    private static Vector3 GetVector3WithOffset(Kinect.Joint joint, Vector3 offset)
    {
        //Node positioning, can be used to set scale
        return new Vector3(joint.Position.X - offset.x, joint.Position.Y - offset.y, -joint.Position.Z - offset.z);
    }
}

