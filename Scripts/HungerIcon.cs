using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerIcon : MonoBehaviour
{
    public Sprite[] Changeimages;
    public Image currImg;
    public Slider hungrySlider;

    // Start is called before the first frame update
    void Start()
    {
        currImg = GetComponent<Image>();
    }

    private void Update()
    {
        ChangeImage();
    }
    void ChangeImage()
    {
        if (hungrySlider.value >= 55f)
            currImg.sprite = Changeimages[0];
        else if (hungrySlider.value <= 50f)
            currImg.sprite = Changeimages[1];
        
        if(hungrySlider.value == 0f)
            currImg.sprite = Changeimages[2];        
    }
}
