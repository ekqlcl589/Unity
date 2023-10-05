using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFDoor : MonoBehaviour
{
    private bool active = false;

    [SerializeField] private GameObject door;

    private const int buildIndex = 1;
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

        if (SceneManager.GetActiveScene().buildIndex == buildIndex)
        {
            if (GameManager.instance.GetLastDay())
                active = true;

            door.SetActive(active);
        }
        else
            this.enabled = false;

    }


}
