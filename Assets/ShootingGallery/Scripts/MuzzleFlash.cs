using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MuzzleFlash : MonoBehaviour
{
    private VisualEffect muzzleFlash;

    void Start()
    {
        muzzleFlash = GetComponent<VisualEffect>();
    }

    public void Fire()
    {
        muzzleFlash.SendEvent("Fire");
    }
}
