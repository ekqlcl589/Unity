using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookingUI : MonoBehaviour
{
    public Slot[] slots; // �ƴ� �ù� �� ���� ���� �� �о� ���°ǵ� ���Ű����� 
    public CookingSlot[] cookSlots;

    //public GameObject changeItem;
    public Item[] item;

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
                if (slots[i].item.itemName == "Steak")// ���ǿ� ������ �־ ������ �ߴµ� �̻��ϰ� ������ ��...
                {
                    slots[i].item = item[0];
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
                if (slots[i].item.itemName == "����")
                {
                    slots[i].item = item[1];
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
                if (slots[i].item.itemName == "�Ҽ���")
                {
                    slots[i].item = item[2];
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
        } // �ӽù��� switch�� �ٸ� ������� ���ϰ� ������ �� �ְ� ���ľ���
    }

    void AddCookCount()
    {
        cookCnt++;

        AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.cook1, cook: cookCnt);
    }
}
