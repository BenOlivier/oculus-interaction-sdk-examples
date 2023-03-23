using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongManager : MonoBehaviour
{
    [SerializeField]
    private TransformAnimator transformAnimator;

    [SerializeField]
    private MusicPlayer musicPlayer;

    [SerializeField]
    private Transform[] songs;

    [SerializeField]
    private float songSpacing = 0.15f;

    [Serializable]
    public struct SongData
    {
        public Texture2D albumArt;
        public string title;
        public string artist;
        public AudioClip song;
    }

    [SerializeField]
    private SongData[] songData;

    private int currentIndex = 0;
    private int newIndex = 1;
    private int albumIndex = 0;

    private bool isAnimating = false;

    [ContextMenu("Previous")]
    public void PreviousSong()
    {
        if (isAnimating) return;

        UpdateIndices(-1);
        UpdateNewSongData(songs[newIndex]);
        songs[newIndex].localPosition = Vector3.left * songSpacing;

        transformAnimator.openTarget = Vector3.right * songSpacing;
        transformAnimator.Open();
        isAnimating = true;

        musicPlayer.UpdateCurrentSong(songData[albumIndex].song);
    }

    [ContextMenu("Next")]
    public void NextSong()
    {
        if (isAnimating) return;

        UpdateIndices(1);
        UpdateNewSongData(songs[newIndex]);
        songs[newIndex].localPosition = Vector3.right * songSpacing;

        transformAnimator.openTarget = Vector3.left * songSpacing;
        transformAnimator.Open();
        isAnimating = true;

        musicPlayer.UpdateCurrentSong(songData[albumIndex].song);
    }

    private void UpdateIndices(int delta)
    {
        albumIndex += delta;
        albumIndex = albumIndex > songData.Length - 1 ?
            0 : albumIndex < 0 ? songData.Length - 1 : albumIndex;
        newIndex = currentIndex == 0 ? 1 : 0;
    }

    private void UpdateNewSongData(Transform song)
    {
        song.gameObject.SetActive(true);
        song.GetComponentInChildren<MeshRenderer>().material.SetTexture("_MainTex", songData[albumIndex].albumArt);
        TMP_Text [] text = song.GetComponentsInChildren<TMP_Text>();
        text[0].text = songData[albumIndex].title;
        text[1].text = songData[albumIndex].artist;
    }

    public void ScrollComplete()
    {
        transformAnimator.transform.localPosition = Vector3.zero;
        songs[newIndex].localPosition = Vector3.zero;
        songs[currentIndex].gameObject.SetActive(false);
        currentIndex = newIndex;
        isAnimating = false;
    }
}
