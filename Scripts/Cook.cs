using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    public static Cook instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<Cook>();
            }

            return m_instance;
        }
        private set { }
    }
    public static Cook m_instance;

    public GameObject cookPanel;

    private bool active = false;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        cookPanel.SetActive(active);

    }

    public void Update()
    {
        ActivePanel();
    }

    public void ActivePanel()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            active = !active;
            cookPanel.SetActive(active);
        }
    }
}
