using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    private int slotnum;
    public void SetSlotNum(int num) { slotnum = num; }

    public Text achivementText; // ��� ���� ���� �ٸ��� ���� 
    public AchievementsManager.Achievements ach;
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
