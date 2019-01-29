using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;



public class Kinect_Body_MappingV3 : MonoBehaviour {

    public GameObject BodySourceManager;
    public int players = 1;

    [SerializeField]
    public GameObject head, leftAnkle, rightAnkle, leftWrist, leftHandTip, rightWrist, rightHandTip, pelvis, leftElbow, rightElbow, leftKnee, rightKnee, chest, leftHip, rightHip, leftHandPos, rightHandPos, leftThumb, rightThumb, rightThumbCopy, leftThumbCopy;

    public float smooth = 1f;

    public static bool mapped = false;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    private Dictionary<Kinect.JointType, GameObject> _Joints;

    private void Start()
    {
        _Joints = new Dictionary<Kinect.JointType, GameObject>()
        {
            { Kinect.JointType.Head, head },
            { Kinect.JointType.ThumbLeft, leftThumb },
            { Kinect.JointType.ThumbRight, rightThumb },
            { Kinect.JointType.SpineShoulder, chest },
            { Kinect.JointType.ElbowLeft, leftElbow },
            { Kinect.JointType.ElbowRight, rightElbow },
            { Kinect.JointType.KneeLeft, leftKnee },
            { Kinect.JointType.KneeRight, rightKnee },
            { Kinect.JointType.HipLeft, leftHip },
            { Kinect.JointType.HipRight, rightHip },
            { Kinect.JointType.SpineBase, pelvis },
            { Kinect.JointType.AnkleLeft, leftAnkle },
            { Kinect.JointType.AnkleRight, rightAnkle },
            { Kinect.JointType.WristLeft, leftHandPos },
            { Kinect.JointType.HandTipLeft, leftHandTip },
            { Kinect.JointType.WristRight, rightHandPos },
            { Kinect.JointType.HandTipRight, rightHandTip },
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
            mapped = false;
            return;
        } else
        {
            mapped = true;
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
                rightThumbCopy.transform.position = rightThumb.transform.localPosition;
                leftThumbCopy.transform.position = leftThumb.transform.localPosition;
                MoveIKGoal(body);
                
            }
        }
    }


    private void MoveIKGoal(Kinect.Body body)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint joint = body.Joints[jt];

            GameObject temp = null;
            if (_Joints.TryGetValue(jt, out temp))
            {
                GameObject jointPos = _Joints[jt];
                jointPos.transform.localPosition = GetVector3FromJoint(joint);
            }


            // HAND ANGLE AND ROTATION
            if (jt == Kinect.JointType.WristLeft) 
            {
                /*Vector3 relativePos = rightThumbCopy.transform.localPosition - leftHandPos.transform.localPosition;
                Vector3 relativePos2 = leftHandTip.transform.localPosition - leftHandPos.transform.position;
                Quaternion rotationY = Quaternion.LookRotation(relativePos, Vector3.up);
                Quaternion rotationXz = Quaternion.LookRotation(relativePos2, Vector3.right);
                leftWrist.transform.localRotation = Quaternion.RotateTowards(leftWrist.transform.localRotation, rotationXz, Time.deltaTime * smooth);
                leftHandPos.transform.localRotation = Quaternion.RotateTowards(leftHandPos.transform.rotation, rotationY, Time.deltaTime * smooth);*/
            }

            if (jt == Kinect.JointType.WristRight)
            {
                Vector3 relativePos = leftThumbCopy.transform.localPosition - rightHandPos.transform.localPosition;
                Vector3 relativePos2 = rightHandTip.transform.localPosition - rightHandPos.transform.localPosition;
                Quaternion rotationY = Quaternion.LookRotation(relativePos, Vector3.up);
                Quaternion rotationXz = Quaternion.LookRotation(relativePos2, Vector3.right);
                rightWrist.transform.localRotation = Quaternion.RotateTowards(rightWrist.transform.localRotation, rotationXz, Time.deltaTime * smooth);
                rightHandPos.transform.localRotation = Quaternion.RotateTowards(rightHandPos.transform.rotation, rotationY, Time.deltaTime * smooth);
            }

            //Hip Rotation
            if (jt == Kinect.JointType.SpineShoulder)
            {
                leftHip.transform.LookAt(rightHip.transform.position);
                pelvis.transform.rotation = leftHip.transform.rotation;
            }
        }

    }

    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        //Node positioning, can be used to set scale
        return new Vector3(joint.Position.X, joint.Position.Y, -joint.Position.Z);
    }
}

