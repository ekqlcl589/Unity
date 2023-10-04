using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; // ���� ���迡���� 50�� = ���� ������ 1�� 

    public bool isNight = false;
    public bool day = false;
    public int dayCount = 0; 
    // ������ ������ ��Ȯ�� �� Ŭ���������� ��� �ϴ���, �ƴϸ� �ٸ� Ŭ�������� �� ���� �޾Ƽ� ������ Ȯ�� �� private�� ���� 
    
    [SerializeField] private float nightFogDensity; // �� ������ �Ȱ� �е�
    private float dayFogDensity;
    [SerializeField] private float fogDensityCalc; // ������ ����
    private float currentFogDensity;

    private const int maxEulerAngleX = 170;
    private const int minEulerAngleX = 10;

    private const float dayCountSecond = 72f; // ī�޶� ȸ�� ���� / ���� �ð�

    private const float rotateTime = 0.1f;

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
        // ������ x ������ ȸ�� ���ǽð� 1�ʿ� rotateTime * secondPerRealTimeSecond ������ŭ ȸ��
        GameManager.instance.isNight = isNight;

        transform.Rotate(Vector3.right, rotateTime * secondPerRealTimeSecond * Time.deltaTime);
        if (transform.eulerAngles.x >= maxEulerAngleX)
            isNight = true;
        else if (transform.eulerAngles.x <= minEulerAngleX)
            isNight = false;

        if (isNight)
        {
            if (currentFogDensity <= nightFogDensity)
            {
                currentFogDensity += rotateTime * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
        else
        {
            if (currentFogDensity >= dayFogDensity)
            {
                currentFogDensity -= rotateTime * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
    }

    private IEnumerator DayCount()
    {
        while(true)
        {
            GameManager.instance.AddDayCount(1);
            yield return new WaitForSeconds(dayCountSecond);

        }

    }
}
