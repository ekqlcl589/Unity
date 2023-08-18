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
    public float startingdata = 0f; // 시작 요리 게이지
    public float cook { get; protected set; } // 현재 요리 게이지

    private bool isCooking = false; // 밖에서 조절?

    private bool isCookOk = false;
    public bool IsCooking
    {
        get => isCookOk;
        set
        {
            isCookOk = value; // 사실상 밖에서는 건드릴일 없음, 없어야 해서 set이면 그냥 프라이빗 해야 함
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
                        Debug.Log("오호..");
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
                        Debug.Log("오호..");
                        isCooking = true;
                    }
                    break;
                }
            case 2:
                {
                    if (CheckItem())
                    {
                        slider.gameObject.SetActive(true);
                        Debug.Log("오호..");
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
                        Debug.Log("오호..");
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
        //        Debug.Log("오호..");
        //        isCooking = true;
        //    }
        //    else
        //        Debug.Log("zz..");
        //}
        //else
        //    Debug.Log("흠.."); // 아이템을 드래그 해서 사용? 아니면 이미지로 만들기는 좀 많이 애매하네...

    }

    public bool CheckItem()
    {
        if(Inventory.instance.Items.Count >= 1) // 특정 아이템의 정보를... 어떻게 가져온담....
        {
            //인벤토리의 아이템을 가져오는 게 아니라 슬롯의 아이템을 가져와서 비교를 한다?
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

            Debug.Log("요리 성공");

            
            slider.gameObject.SetActive(false);

            isCookOk = true;
            isCooking = false;
        }
    }
}
