using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드
using UnityEngine.Android;
// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
        private set { }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text ammoText; // 탄약 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 
    public Text aliveDayText; // 며칠간 살아서 행동했는지 알리는 텍스트
    public Text dieReasonText; // 죽은 이유, 잡은 좀비의 수 표시용 텍스트
    public GameObject lastDatUI;
    public GameObject safeHouseUI;
    public GameObject bossAlramUI;
    public Text dayText;
    public Text killText;

    public Text GetKillText() { return killText; }


    // 탄약 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    public void UpdateHPBar(bool active)
    {
        gameoverUI.SetActive(active);
    }
    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    public void UpdateAliveDayText(int dayCount)
    {
        aliveDayText.text = "당신은 " + dayCount + "일 동안 생존하였습니다";
    }
    public void UpdateDieReasonText(int zombieCount)
    {
        dieReasonText.text = "처치한 좀비 수: " + zombieCount;
    }

    public void UpdateAliveText(int newAlive)
    {
        dayText.text = "생존" + newAlive + "일차";
    }

    public void UpdateKillText(int newKill)
    {
        killText.text = "좀비" + newKill + "마리 사냥";
    }

    public void ActivZombieParade(bool active)
    {
        lastDatUI.SetActive(active);
    }

    public void ActiveSafeHouse(bool active)
    {
        safeHouseUI.SetActive(active);
    }

    public void ActivBossUI(bool active)
    {
        bossAlramUI.SetActive(active);
    }
    // 로딩씬 게임 시작
    public void GameStart()
    {
        LoadingSceneController.LoadScene("Main");
    }

    // 게임 재시작
    public void GameRestart()
    {
        GameManager.instance.ReGame();
        LoadingSceneController.LoadScene("Main");
    }

    // 게임 종료 
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}