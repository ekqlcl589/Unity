using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    private int slotnum;
    public int SlotNum
    {
        get
        {
            return slotnum;
        }
        set
        {
            if (slotnum == value)
                return;

            slotnum = value;
        }
    }

    public Text achivementText;

    private AchievementsManager.Achievements ach;

    public AchievementsManager.Achievements Ach
    {
        get
        {
            return ach;
        }
        set
        {
            if (ach == value)
                return;

            ach = value;
        }
    }

    public void UpdateSlotUI()
    {
        if (ach == AchievementsManager.Achievements.kill1)
            achivementText.text = "���� ��ɲ�";
        else if (ach == AchievementsManager.Achievements.food1)
            achivementText.text = "������ �ķ�";
        else if (ach == AchievementsManager.Achievements.cook1)
            achivementText.text = "�丮�� ����";
        else if (ach == AchievementsManager.Achievements.kill10)
            achivementText.text = "���� �л���";
        else if (ach == AchievementsManager.Achievements.day3)
            achivementText.text = "������";
        else if (ach == AchievementsManager.Achievements.day7)
            achivementText.text = "������ ����";
        else if (ach == AchievementsManager.Achievements.specialFood)
            achivementText.text = "�Ϻ��� ����!";
        else if (ach == AchievementsManager.Achievements.sunKill)
            achivementText.text = "�Ͼີ";
        else if (ach == AchievementsManager.Achievements.safeHouse)
            achivementText.text = "����ó ����";
    }

}
