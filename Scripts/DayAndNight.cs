using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; // ���� ���迡���� 50�� = ���� ������ 1�� 

    public bool isNight = false;
    public bool day = false;
    public int dayCount = 0;
    
    [SerializeField] private float nightFogDensity; // �� ������ �Ȱ� �е�
    private float dayFogDensity;
    [SerializeField] private float fogDensityCalc; // ������ ����
    private float currentFogDensity;

    public delegate void OnDayCount();
    public OnDayCount onDayCount;
    // Start is called before the first frame update
    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
        StartCoroutine(DayCount());

    }

    // Update is called once per frame
    void Update()
    {
        // ������ x ������ ȸ�� ���ǽð� 1�ʿ� 0.1f * secondPerRealTimeSecond ������ŭ ȸ��
        GameManager.instance.isNight = isNight;

        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);
        if (transform.eulerAngles.x >= 170)
            isNight = true;
        else if (transform.eulerAngles.x <= 10)
            isNight = false;

        if (isNight)
        {
            if (currentFogDensity <= nightFogDensity)
            {
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
        else
        {
            if (currentFogDensity >= dayFogDensity)
            {
                currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
    }

    private IEnumerator DayCount()
    {
        while(true)
        {
            //dayCount += 1;
            GameManager.instance.AddDayCount(1);
            yield return new WaitForSeconds(72f);

        }

    }
}
