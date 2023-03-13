using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIconToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject playIcon;

    [SerializeField]
    private GameObject pauseIcon;

    private bool isPlayIcon = false;

    public void ToggleIcon()
    {
        if (isPlayIcon)
        {
            playIcon.SetActive(false);
            pauseIcon.SetActive(true);
            isPlayIcon = true;
        }
        else
        {
            playIcon.SetActive(true);
            pauseIcon.SetActive(false);
            isPlayIcon = false;
        }
    }
}
