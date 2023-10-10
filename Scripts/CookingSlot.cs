using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookingSlot : MonoBehaviour, IPointerUpHandler
{

    public int slotNum;

    public Slider slider;

    private const float maxdata = 100f;
    private const float startingdata = 0f; // ���� �丮 ������

    private const float delayTime = 0.05f;
    public float cook { get; protected set; } // ���� �丮 ������

    private bool isCooking = false; // �ۿ��� ����?

    private bool isCookOk = false;
    public bool IsCooking
    {
        get => isCookOk;
        set
        {
            if(isCooking == value) return;

            isCookOk = value;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCooking();

    }

    public void OnEnable()
    {
        cook = startingdata;

        slider.gameObject.SetActive(false);

        slider.maxValue = maxdata;

        slider.value = cook;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        switch (slotNum)
        {
            case 0:
                {
                    if (CheckItem())
                    {
                        slider.gameObject.SetActive(true);
                        isCooking = true;
                    }
                    break;
                }

            case 1:
                {
                    if (CheckItem())
                    {
                        slider.gameObject.SetActive(true);
                        isCooking = true;
                    }
                    break;
                }
            case 2:
                {
                    if (CheckItem())
                    {
                        slider.gameObject.SetActive(true);
                        isCooking = true;
                    }
                    break;
                }

            case 3:
                {
                    if (CheckItem())
                    {
                        slider.gameObject.SetActive(true);
                        isCooking = true;
                    }

                    break;
                }

        }
    }

    public bool CheckItem()
    {
        if (Inventory.Instance.Items.Count >= 1) // Ư�� �������� ������... 
        {
            //�κ��丮�� �������� �������� �� �ƴ϶� ������ �������� �����ͼ� �񱳸� �Ѵ�?
            //Inventory.instance.Items.FindIndex(slotNum => slotNum.Equals(0));
            return true;
        }

        return false;
    }

    public void Cooking()
    {
        cook += Time.time * delayTime;
        slider.value = cook;

        if (cook >= maxdata)
        {
            cook = 0f;
            slider.value = cook;

            Debug.Log("�丮 ����");

            slider.gameObject.SetActive(false);

            isCookOk = true;
            isCooking = false;
        }
    }

    public void CheckCooking()
    {
        if (isCooking)
            Cooking();

    }
}
