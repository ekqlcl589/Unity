using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    public static Cook instance;

    public GameObject cookPanel;
    bool active = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

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
