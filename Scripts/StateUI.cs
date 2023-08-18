using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUI : MonoBehaviour
{
    public GameObject stateUI;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        stateUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        stateUI.SetActive(active);
    }

    public void UpdateState()
    {
        active = true;
    }
}
