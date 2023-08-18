using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    private static PlayerCamera instance;

    public GameObject tPlayer;
    public Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        tPlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");
            if (tPlayer != null)
            {
                tFollowTarget = tPlayer.transform;
                vcam.Follow = tFollowTarget;
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 direction = (tPlayer.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("EnvironmentObject"));

        for(int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for(int j = 0; j < obj.Length; j++)
            {
                //if(tPlayer.transform.position.y >= obj[j].transform.position.y)
                    obj[j]?.BecomeTransparent();

            }
        }
    }
}
