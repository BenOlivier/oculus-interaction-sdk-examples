using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIconToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject playIcon;

    [SerializeField]
    private GameObject pauseIcon;

    private bool isPlayIcon = true;

    public void ToggleIcon()
    {
        if (isPlayIcon)
        {
            playIcon.SetActive(false);
            pauseIcon.SetActive(true);
            isPlayIcon = false;
        }
        else
        {
            playIcon.SetActive(true);
            pauseIcon.SetActive(false);
            isPlayIcon = true;
        }
    }
}
