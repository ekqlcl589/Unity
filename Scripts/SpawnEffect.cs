using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material mtrlOrg;
    [SerializeField] private Material mtrlPhase;
    [SerializeField] private float phaseTime = 2f;

    private const int phaseStart = 0;
    private const int phaseEnd = 17;

    // Start is called before the first frame update
    void Start()
    {
        _renderer.material = mtrlPhase;
        DoPhase(phaseStart, phaseEnd, phaseTime);
    }

    void DoPhase(float start, float dest, float time) // 매번 값 갱신, 갱신 될 때마다 호출되는 콜백함수 역할
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", start, "to", dest, "time", time,
            "onupdatetarget", gameObject,
            "onupdate", "TweenOnUpdate",
            "oncomplte", "TweenOnComplte",
            "easetype", iTween.EaseType.easeInOutCubic));
    }

    void TweenOnUpdate(float value)
    {
        _renderer.material.SetFloat("_SpllitValue", value);
    }

    void TweenOnComplte()
    {
        _renderer.material = mtrlOrg;
    }
}
