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
    }
    public static Cook m_instance;

    [SerializeField] private GameObject cookPanel;

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
        if (Input.GetKeyDown(KeyCode.F))// && isCooking)
        {
            active = !active;
            cookPanel.SetActive(active);
        }

    }
}
