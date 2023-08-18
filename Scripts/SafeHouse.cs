using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeHouse : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.safeHouse,
                safeHouse: 1);

            GameManager.instance.SafeHouse = true;

            UIManager.instance.dayText.text = "생존 성공";
        }
    }
}
