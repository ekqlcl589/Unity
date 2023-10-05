using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteText : MonoBehaviour
{
    [SerializeField] private float time;

    private const float num1 = 1f;

    private const float _time = 1f;
    private const float _halfTime = 0.5f;
    private const float _zeroTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (time < _halfTime)
        {
            GetComponent<Text>().color = new Color(num1, num1, num1, num1 - time);
        }
        else
        {
            GetComponent<Text>().color = new Color(num1, num1, num1, time);
            if (time > _time)
                time = _zeroTime;
        }

        time += Time.deltaTime;
    }
}
