using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    private const int slotnum = 8;
    private static Achievement m_instance;

    public static Achievement instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<Achievement>();
            }
            return m_instance;
        }
    }

    public List<AchievementsManager.Achievements> achievements = new List<AchievementsManager.Achievements>();

    private int achievementSlot;

    public delegate void OnSlotCountChange(int value);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeAch();
    public OnChangeAch onChangeAch;

    public List<AchievementsManager.Achievements> Achievements
    {
        get => achievements;
        set
        {
            achievements = value;
            onSlotCountChange.Invoke(achievementSlot);

        }
    }
    public int AchievementSlot
    {
        get => achievementSlot;
        set
        {
            achievementSlot = value;
            onSlotCountChange.Invoke(achievementSlot);
        }
    }
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        achievementSlot = slotnum;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Addachievement(AchievementsManager.Achievements ach)
    {
        if (achievements.Count < AchievementSlot)
        {
            achievements.Add(ach);
            if (onChangeAch != null)
                onChangeAch.Invoke();
            return true;
        }
        return false;
    }
}
