using System.Collections.Generic;
using UnityEngine;
using static OVRPlugin;


public class FingerPosition : MonoBehaviour
{
    public enum HandToTrack
    {
        Left,
        Right
    }
    [HideInInspector]
    public Vector3 Position = Vector3.zero;

    [SerializeField]
    private HandToTrack handToTrack = HandToTrack.Left;

    [SerializeField]
    private GameObject objectToTrackMovement;

    private OVRSkeleton ovrSkeleton;

    private Transform boneTransform;

//#if !UNITY_EDITOR

    private void Awake()
    {
        ovrSkeleton = objectToTrackMovement.GetComponent<OVRSkeleton>();
    }

    private void Update()
    {
        if (boneTransform == null)
        {
            foreach (var bone in ovrSkeleton.Bones)
            {
                print(bone);
                if (bone.Id == OVRSkeleton.BoneId.Hand_IndexTip)
                {
                    boneTransform = bone.Transform;
                }
            }
        }
        else Position = boneTransform.position;
    }

//#endif

}
