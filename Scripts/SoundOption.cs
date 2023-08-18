using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOption : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider bgmSlider;
    public Slider sfxSlider;

    public GameObject soundPanel;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        soundPanel.SetActive(active);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            active = !active;
            soundPanel.SetActive(active);
        }

    }

    public void SetBgmVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
    }
    public void SetSfxVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(bgmSlider.value) * 20);
    }

}
