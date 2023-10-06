using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOption : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject soundPanel;
    private bool active = false;

    private float amplification = 20f;

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
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * amplification);
    }
    public void SetSfxVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(bgmSlider.value) * amplification);
    }

}
