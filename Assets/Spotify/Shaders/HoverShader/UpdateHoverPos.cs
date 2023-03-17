using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class UpdateHoverPos : MonoBehaviour
{
    [SerializeField]
    private Material material;

    private OVRPlugin.Skeleton skeleton;

    private void Update()
    {
        if (OVRPlugin.GetSkeleton(OVRPlugin.SkeletonType.HandLeft, out skeleton))
            foreach (var bone in skeleton.Bones)
                if (bone.Id == OVRPlugin.BoneId.Hand_IndexTip)
                    material.SetVector("_LeftHandPos", new Vector3(
                        bone.Pose.Position.x, bone.Pose.Position.y, bone.Pose.Position.z));

        if (OVRPlugin.GetSkeleton(OVRPlugin.SkeletonType.HandRight, out skeleton))
            foreach (var bone in skeleton.Bones)
                if (bone.Id == OVRPlugin.BoneId.Hand_IndexTip)
                    material.SetVector("_RightHandPos", new Vector3(
                        bone.Pose.Position.x, bone.Pose.Position.y, bone.Pose.Position.z));
    }
}
