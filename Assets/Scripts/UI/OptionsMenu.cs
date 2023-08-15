using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    [SerializeField] Toggle smoothTransitions;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            SetVolume(PlayerPrefs.GetFloat("Volume"));
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }

        if (PlayerPrefs.HasKey("SmoothTransitionsSelected"))
        {
            if (PlayerPrefs.GetInt("SmoothTransitionsSelected") == 1)
            {
                smoothTransitions.isOn = true;
            }
            else if (PlayerPrefs.GetInt("SmoothTransitionsSelected") == 0)
            {
                smoothTransitions.isOn = false;
            }
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void smoothTransitionsSelected()
    {
        if (smoothTransitions.isOn)
            PlayerPrefs.SetInt("SmoothTransitionsSelected", 1);
        else if (!smoothTransitions.isOn)
            PlayerPrefs.SetInt("SmoothTransitionsSelected", 0);
    }
}
