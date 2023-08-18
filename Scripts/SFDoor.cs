using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFDoor : MonoBehaviour
{
    bool active = false;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(active);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (door == null)
            return;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (GameManager.instance.clear)
                active = true;

            door.SetActive(active);
        }
        else
            this.enabled = false;

    }


}
