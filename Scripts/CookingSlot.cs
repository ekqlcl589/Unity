using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookingSlot : MonoBehaviour, IPointerUpHandler
{
    
    public int slotNum;
    
    public Slider slider;

    public float maxdata = 100f;
    public float startingdata = 0f; // ���� �丮 ������
    public float cook { get; protected set; } // ���� �丮 ������

    private bool isCooking = false; // �ۿ��� ����?

    private bool isCookOk = false;
    public bool IsCooking
    {
        get => isCookOk;
        set
        {
            isCookOk = value; // ��ǻ� �ۿ����� �ǵ帱�� ����, ����� �ؼ� set�̸� �׳� �����̺� �ؾ� ��
        }
    }

    void Start()
    {
        //Inventory.instance = instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooking)
            Cooking();

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
        switch(slotNum)
        {
            case 0:
                {
                    if (CheckItem())
                    {
                        slider.gameObject.SetActive(true);
                        Debug.Log("��ȣ..");
                        isCooking = true;
                    }
                    else
                        Debug.Log("zz..");

                     break;
                }

            case 1:
                {
                    if (CheckItem())
                    {
                        slider.gameObject.SetActive(true);
                        Debug.Log("��ȣ..");
                        isCooking = true;
                    }
                    break;
                }
            case 2:
                {
                    if (CheckItem())
                    {
                        slider.gameObject.SetActive(true);
                        Debug.Log("��ȣ..");
                        isCooking = true;
                    }
                    else
                        Debug.Log("zz..");

                    break;
                }

            case 3:
                {
                    if (CheckItem())
                    {
                        slider.gameObject.SetActive(true);
                        Debug.Log("��ȣ..");
                        isCooking = true;
                    }
                    else
                        Debug.Log("zz..");

                }
                break;

        }
        //if (slotNum == 3)
        //{
        //    if(CheckItem())
        //    {
        //        slider.gameObject.SetActive(true);
        //        Debug.Log("��ȣ..");
        //        isCooking = true;
        //    }
        //    else
        //        Debug.Log("zz..");
        //}
        //else
        //    Debug.Log("��.."); // �������� �巡�� �ؼ� ���? �ƴϸ� �̹����� ������ �� ���� �ָ��ϳ�...

    }

    public bool CheckItem()
    {
        if(Inventory.instance.Items.Count >= 1) // Ư�� �������� ������... ��� �����´�....
        {
            //�κ��丮�� �������� �������� �� �ƴ϶� ������ �������� �����ͼ� �񱳸� �Ѵ�?
            //Inventory.instance.Items.FindIndex(slotNum => slotNum.Equals(0));
            return true;
        }
        
        return false;
    }

    public void Cooking()
    {
        cook += Time.time * 0.05f;
        slider.value = cook;

        if(cook >= maxdata)
        {
            cook = 0f;
            slider.value = cook;

            Debug.Log("�丮 ����");

            
            slider.gameObject.SetActive(false);

            isCookOk = true;
            isCooking = false;
        }
    }
}
