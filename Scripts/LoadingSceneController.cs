using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;


    public Image progressBar;

    private const float loadingWaitTime = 0.01f;

    private float progressbarDelay = 0.9f; // �񵿱� �ε��̶� �� ���Ƿ� ����

    private const float progressbarComplate = 1f;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); // ���� �񵿱�� �θ�
        op.allowSceneActivation = false; // ���� �񵿱�� �ҷ����� �� �ڵ����� �ҷ��� ������ �̵��� ������ ���� �ϴ� �� false�� ���� 90 ���� �θ��� ��ٸ� true�� �ٲٸ� ���� �ҷ��� ��
        // �ε��� �ʹ� ������ �� ȭ���� �ʹ� ���� �������� false�� �����ؼ� ����ũ �ε��� �־���
        // �ε�ȭ�鿡�� �ҷ��;� �ϴ� �� �� �� �ִ°� �ƴ� ������ Ŀ���� ���� ����� ������ �θ��� �̰� �о� �;� �ϴµ� 
        // ���ҽ� �ε��� ������ ���� �� �ε��� ������ ������Ʈ���� ������ ����
        float timer = 0f;
        while (!op.isDone)
        {
            yield return new WaitForSeconds(loadingWaitTime); // �� ���ָ� �ٰ� �Ѿ�� �� �� ����

            if (op.progress < progressbarDelay)
            {
                progressBar.fillAmount = op.progress;
            }
            else // ����ũ �ε� 1�ʰ� ä���� ���� �ҷ���
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(progressbarDelay, progressbarComplate, timer); // 1�ʿ� ���ļ� ä����
                if (progressBar.fillAmount >= progressbarComplate)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
