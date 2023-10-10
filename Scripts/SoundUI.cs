using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public GameObject soundPanel;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        soundPanel.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        ActivePanel();
    }

    public void ActivePanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            active = !active;
            soundPanel.SetActive(active);
        }

    }
}
