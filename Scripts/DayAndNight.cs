using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; // 게임 세계에서의 50초 = 현실 세계의 1초 

    public bool isNight = false;
    public bool day = false;
    public int dayCount = 0; 
    // 에디터 열리면 정확히 이 클래스에서만 사용 하는지, 아니면 다른 클래스에서 이 값을 받아서 쓰는지 확인 후 private로 변경 
    
    [SerializeField] private float nightFogDensity; // 밤 상태의 안개 밀도
    private float dayFogDensity;
    [SerializeField] private float fogDensityCalc; // 증감량 비율
    private float currentFogDensity;

    private const int maxEulerAngleX = 170;
    private const int minEulerAngleX = 10;

    private const float dayCountSecond = 72f; // 카메라 회전 각도 / 게임 시간

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
        // 조명을 x 축으로 회전 현실시간 1초에 rotateTime * secondPerRealTimeSecond 각도만큼 회전
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
