using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드

public class ReGame : MonoBehaviour
{
    public void ReGameStart()
    {
        SceneManager.LoadScene("SplasScene");

    }
}
