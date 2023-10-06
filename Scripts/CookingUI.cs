using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookingUI : MonoBehaviour
{
    [SerializeField] private Slot[] slots;
    [SerializeField] private CookingSlot[] cookSlots;

    [SerializeField] private Item[] item;

    private int cookCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        //slots = GetComponentsInChildren<Slot>();
        //cookSlots = GetComponentsInChildren<CookingSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cookSlots[0].IsCooking)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].GetItem().ItemName == "Steak")
                {
                    slots[i].SetItemData(item[0]);
                    slots[i].UpdateSlotUI();
                    cookSlots[0].IsCooking = false;
                    AddCookCount();

                    return;
                }
                else
                {
                    cookSlots[0].IsCooking = false;

                    return;
                }
            }
        }
        else if (cookSlots[1].IsCooking)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].GetItem().ItemName == "생닭")
                {
                    slots[i].SetItemData(item[1]);
                    slots[i].UpdateSlotUI();
                    cookSlots[1].IsCooking = false;
                    AddCookCount();

                    return;
                }
                else
                {
                    cookSlots[1].IsCooking = false;

                    return;

                }
            }

        }
        else if (cookSlots[2].IsCooking)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].GetItem().ItemName == "소세지")
                {
                    slots[i].SetItemData(item[2]);
                    slots[i].UpdateSlotUI();
                    cookSlots[2].IsCooking = false;
                    AddCookCount();

                    return;
                }
                else
                {
                    cookSlots[2].IsCooking = false;

                    return;

                }
            }
        } // 임시방편 switch나 다른 방법으로 편하게 구분할 수 있게 고쳐야함
    }

    void AddCookCount()
    {
        cookCnt++;

        AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.cook1, cook: cookCnt);
    }
}
