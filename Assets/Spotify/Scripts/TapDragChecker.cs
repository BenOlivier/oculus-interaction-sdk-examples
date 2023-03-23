using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDragChecker : MonoBehaviour
{
    [SerializeField]
    private FingerPosition _fingerPosition;

    [SerializeField]
    private GameObject _volumeSlider;

    private Vector3 _dragStartPos;

    private bool _isDragging = false;

    public void PinchStarted()
    {
        _dragStartPos = _fingerPosition.Position;

        _volumeSlider.SetActive(true);

        _volumeSlider.transform.position = _dragStartPos;

        _isDragging = true;
    }

    public void PinchEnded()
    {
        _volumeSlider.SetActive(false);

        _isDragging = false;
    }
}
