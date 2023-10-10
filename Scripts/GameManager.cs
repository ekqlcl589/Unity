using System.Collections;
using UnityEngine;

// 점수와 게임 오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviour
{
    public System.Action action;

    // 싱글톤 접근용 프로퍼티

    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
        private set { }
    }

    public bool IsGameover { get; private set; } // 게임 오버 상태


    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    private int zombieCount = 0;

    public int ZombieCount { get { return zombieCount; } private set { } }

    private int dayCount = 0;

    private int weaponNum = 0;

    private const int weaponCount = 0;
    private const int weaponCount1 = 1;
    public int WeaponNum { get { return weaponNum; } private set { } }

    private bool isNight = false;

    public bool IsNight { get { return isNight; } private set { } }

    private bool last = true;

    public bool Last { get { return last; } private set { } }

    private bool safeHouse = false;

    public bool SafeHouse { get { return safeHouse; } private set { } }

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임 오버
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    public void AddDayCount(int newDayCount)
    {
        if (!IsGameover)
        {
            dayCount += newDayCount;

            UIManager.instance.UpdateAliveDayText(dayCount);
            UIManager.instance.UpdateAliveText(dayCount);

            AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.day3,
                day: dayCount);

            AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.day7,
                day: dayCount);

        }
    }
    public void AddZombieCount(int newCount)
    {
        if (!IsGameover)
        {
            zombieCount += newCount;

            UIManager.instance.UpdateDieReasonText(zombieCount);
            UIManager.instance.UpdateKillText(zombieCount);
        }
    }

    public void WeaponNumChange(bool change)
    {
        if (change)
            WeaponNum = weaponCount;
        else
            WeaponNum = weaponCount1;
    }

    public void SetSafeHouseData()
    {
        safeHouse = true;
    }

    public void LastCheckData()
    {
        last = false;
    }

    public void NightCheckData(bool check)
    {
        isNight = check;
    }
    // 게임 오버 처리
    public void EndGame()
    {
        // 게임 오버 상태를 참으로 변경
        IsGameover = true;
        // 게임 오버 UI를 활성화
        UIManager.instance.SetActiveGameoverUI(true);

    }
    public void ReGame()
    {
        Destroy(gameObject);
        IsGameover = false;
        UIManager.instance.SetActiveGameoverUI(false);
    }
}