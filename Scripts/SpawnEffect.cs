using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material mtrlOrg;
    [SerializeField] private Material mtrlPhase;
    [SerializeField] private float PhaseTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _renderer.material = mtrlPhase;
        DoPhase(0, 17, PhaseTime);
    }

    void DoPhase(float start, float dest, float time) // �Ź� �� ����, ���� �� ������ ȣ��Ǵ� �ݹ��Լ� ����
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