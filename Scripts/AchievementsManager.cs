using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AchievementsManager : MonoBehaviour
{
    private const int killCount = 3;
    private const int achievement_hideTextTime = 5;
    private const achievement_1Count = 1;
    private const achievement_3Count = 3;
    private const achievement_5Count = 5;
    private const achievement_7Count = 7;
    private const achievement_10Count = 10;

    Text _achievementText;
    public Text achievementText
    {
        get
        {
            if (_achievementText == null)
            {
                GameObject obj = GameObject.Find("HUD Canvas/Test");
                if (obj != null)
                {
                    _achievementText = obj.GetComponent<Text>();
                }
            }
            return _achievementText;
        }
    }
    public enum Achievements
    {
        kill1,
        food1,
        cook1,
        kill10,
        day3,
        day7,
        specialFood,
        sunKill,
        safeHouse,
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        _achievementText = null;
    }

    class AchievementsComparer : IEqualityComparer<Achievements>
    {
        public bool Equals(Achievements a, Achievements b)
        {
            return a == b;
        }
        public int GetHashCode(Achievements obj)
        {
            return ((int)obj).GetHashCode();
        }
    }

    Dictionary<Achievements, bool> _dicAchievementUnlock =
        new Dictionary<Achievements, bool>(new AchievementsComparer());

    static AchievementsManager _instance;
    public static AchievementsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("AchievementsManager");
                _instance = obj.AddComponent<AchievementsManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public void OnNotify(Achievements achv, int kill = 0, int food = 0, int cook = 0, int day = 0, int special = 0, int sun = 0, int safeHouse = 0)
    {
        switch (achv)
        {
            case Achievements.kill1:
                UnlockKill1(kill);
                break;

            case Achievements.food1:
                UnlockFood(food);
                break;

            case Achievements.cook1:
                UnlockCook(cook);
                break;

            case Achievements.kill10:
                UnlockKill10(kill);
                break;

            case Achievements.day3:
                UnlockAlive3(day);
                break;

            case Achievements.day7:
                UnlockAlive7(day);
                break;

            case Achievements.specialFood:
                UnlockSpecialFood(special);
                break;

            case Achievements.sunKill:
                UnlockSunKill(sun);
                break;

            case Achievements.safeHouse:
                UnlockSafeHouse(safeHouse);
                break;

        }
    }

    public AchievementsManager()
    {
        foreach (Achievements achv in Enum.GetValues(typeof(Achievements)))
        {
            _dicAchievementUnlock[achv] = false;
        }
    }

    void UnlockKill1(int toKill)
    {
        if (_dicAchievementUnlock[Achievements.kill1])
            return;

        if (toKill >= 3)
        {
            StartCoroutine(Cor_ShowText5Sec("업적 달성! \n 좀비 사냥꾼 \n"));
            _dicAchievementUnlock[Achievements.kill1] = true;
            Achievement.instance.Addachievement(Achievements.kill1);
        }
    }

    void UnlockFood(int food)
    {
        if (_dicAchievementUnlock[Achievements.food1])
            return;

        if (food >= defaultAchievementCount)
        {
            StartCoroutine(Cor_ShowText5Sec("업적 달성! \n 최초의 식량 \n"));
            _dicAchievementUnlock[Achievements.food1] = true;
            Achievement.instance.Addachievement(Achievements.food1);
        }
    }

    void UnlockCook(int cook)
    {
        if (_dicAchievementUnlock[Achievements.cook1])
            return;

        if (cook >= defaultAchievementCount)
        {
            StartCoroutine(Cor_ShowText5Sec("업적 달성! \n 요리사 등장 \n"));
            _dicAchievementUnlock[Achievements.cook1] = true;
            Achievement.instance.Addachievement(Achievements.cook1);
        }
    }

    void UnlockKill10(int kill)
    {
        if (_dicAchievementUnlock[Achievements.kill10])
            return;

        if (kill >= 10)
        {
            StartCoroutine(Cor_ShowText5Sec("업적 달성! \n 좀비 학살자 \n"));
            _dicAchievementUnlock[Achievements.kill10] = true;
            Achievement.instance.Addachievement(Achievements.kill10);
        }
    }

    void UnlockAlive3(int day)
    {
        if (_dicAchievementUnlock[Achievements.day3])
            return;

        if (day >= 3)
        {
            StartCoroutine(Cor_ShowText5Sec("업적 달성! \n 생존가 \n"));
            _dicAchievementUnlock[Achievements.day3] = true;
            Achievement.instance.Addachievement(Achievements.day3);
        }

    }
    void UnlockAlive7(int day)
    {
        if (_dicAchievementUnlock[Achievements.day7])
            return;

        if (day >= 7)
        {
            StartCoroutine(Cor_ShowText5Sec("업적 달성! \n 생활의 달인 \n"));
            _dicAchievementUnlock[Achievements.day7] = true;
            Achievement.instance.Addachievement(Achievements.day7);
        }

    }

    void UnlockSpecialFood(int food)
    {
        if (_dicAchievementUnlock[Achievements.specialFood])
            return;

        if (food >= defaultAchievementCount)
        {
            StartCoroutine(Cor_ShowText5Sec("업적 달성! \n 완벽한 음식..? \n"));
            _dicAchievementUnlock[Achievements.specialFood] = true;
            Achievement.instance.Addachievement(Achievements.specialFood);
        }

    }

    void UnlockSunKill(int kill)
    {
        if (_dicAchievementUnlock[Achievements.sunKill])
            return;

        if (kill >= defaultAchievementCount)
        {
            StartCoroutine(Cor_ShowText5Sec("업적 달성! \n 돋보기 실험 \n"));
            _dicAchievementUnlock[Achievements.sunKill] = true;
            Achievement.instance.Addachievement(Achievements.sunKill);
        }
    }

    void UnlockSafeHouse(int safe)
    {
        if (_dicAchievementUnlock[Achievements.safeHouse])
            return;

        if (safe >= defaultAchievementCount)
        {
            StartCoroutine(Cor_ShowText5Sec("업적 달성! \n 은신처 도착\n"));
            _dicAchievementUnlock[Achievements.safeHouse] = true;
            Achievement.instance.Addachievement(Achievements.safeHouse);
        }

    }
    IEnumerator Cor_ShowText5Sec(string text)
    {
        achievementText.text += text;
        yield return new WaitForSeconds(textTime);
        achievementText.text = string.Empty;
    }
}
