using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeHouse : MonoBehaviour
{
    private const int safePoint = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.safeHouse,
                safeHouse: safePoint);

            GameManager.instance.SetSafeHouseData();

            UIManager.instance.GetKillText().text = "생존 성공";
        }
    }
}
