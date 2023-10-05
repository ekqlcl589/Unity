using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    private int slotnum;
    public void SetSlotNum(int num) { slotnum = num; }

    public Text achivementText; // 얘는 슬롯 마다 다르게 적용 
    public AchievementsManager.Achievements ach;
    public void UpdateSlotUI()
    {
        if (ach == AchievementsManager.Achievements.kill1)
            achivementText.text = "좀비 사냥꾼";
        else if (ach == AchievementsManager.Achievements.food1)
            achivementText.text = "최초의 식량";
        else if (ach == AchievementsManager.Achievements.cook1)
            achivementText.text = "요리사 등장";
        else if (ach == AchievementsManager.Achievements.kill10)
            achivementText.text = "좀비 학살자";
        else if (ach == AchievementsManager.Achievements.day3)
            achivementText.text = "생존가";
        else if (ach == AchievementsManager.Achievements.day7)
            achivementText.text = "생존의 달인";
        else if (ach == AchievementsManager.Achievements.specialFood)
            achivementText.text = "완벽한 음식!";
        else if (ach == AchievementsManager.Achievements.sunKill)
            achivementText.text = "뙤약볕";
        else if (ach == AchievementsManager.Achievements.safeHouse)
            achivementText.text = "은신처 도착";
    }

}
