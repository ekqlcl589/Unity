using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AchievementUI : MonoBehaviour
{
    Achievement achievement;

    private bool active = false;
    [SerializeField] private GameObject AchievementPanel;

    [SerializeField] private AchievementSlot[] slots;
    // Start is called before the first frame update
    private void Awake()
    {
        slots = GetComponentsInChildren<AchievementSlot>();
    }
    void Start()
    {
        achievement = Achievement.instance;
        achievement.onSlotCountChange += SlotChange;
        achievement.onChangeAch += RedrawSlotUI;
        AchievementPanel.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            active = !active;
            AchievementPanel.SetActive(active);
        }

    }

    private void SlotChange(int value)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSlotNum(i);
            if (i < achievement.AchievementSlot)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;

        }
    }

    void RedrawSlotUI()
    {
        //for(int i = 0; i < slots.Length; i++) // 애는 슬롯을 삭제할 일이 없어서 안 해도 됨?
        //{
        //    slots[i].RemoveSlot();
        //}
        for (int i = 0; i < achievement.achievements.Count; i++)
        {
            slots[i].ach = achievement.achievements[i];
            slots[i].UpdateSlotUI();
        }
    }
}
