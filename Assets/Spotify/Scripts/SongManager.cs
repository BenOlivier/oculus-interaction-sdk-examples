using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField]
    private TransformAnimator transformAnimator;

    public int songIndex = 0;

    private Vector3 prevDirection = new Vector3(0.15f, 0, 0);
    private Vector3 nextDirection = new Vector3(-0.15f, 0, 0);

    [ContextMenu("Previous")]
    public void PreviousSong()
    {
        transformAnimator.closedTarget = transform.localPosition;
        transformAnimator.openTarget = transform.localPosition + prevDirection;
        transformAnimator.Open();

        songIndex--;
        print("Currently listening to song" + songIndex);
    }

    [ContextMenu("Next")]
    public void NextSong()
    {
        transformAnimator.closedTarget = transform.localPosition;
        transformAnimator.openTarget = transform.localPosition + nextDirection;
        transformAnimator.Open();

        songIndex++;
        print("Currently listening to song" + songIndex);
    }
}
