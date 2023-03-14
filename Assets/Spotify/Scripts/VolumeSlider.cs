using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private LineRenderer fillLineRenderer;

    [SerializeField]
    private Transform handle;

    [SerializeField]
    private MusicPlayer musicPlayer;

    private Vector3[] points = new Vector3[2];

    [HideInInspector]
    public float sliderValue = 0f;

    private bool isGrabbed = false;

    private void Start()
    {
        points[0] = Vector3.down * 0.5f;
        sliderValue = handle.localPosition.y;
        musicPlayer.UpdateVolume(sliderValue);
    }

    private void Update()
    {
        if (isGrabbed) UpdateLine();
    }

    public void GrabSlider()
    {
        isGrabbed = true;
    }

    public void ReleaseSlider()
    {
        isGrabbed = false;
    }

    private void UpdateLine()
    {
        sliderValue = Mathf.InverseLerp(-0.5f, 0.5f, handle.localPosition.y);
        points[1] = new Vector3(0, handle.localPosition.y, 0);
        fillLineRenderer.SetPositions(points);
        musicPlayer.UpdateVolume(sliderValue);
    }
}
