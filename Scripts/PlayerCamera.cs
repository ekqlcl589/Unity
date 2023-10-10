using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    private static PlayerCamera instance;

    private GameObject tPlayer;
    private Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;

    public static PlayerCamera Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerCamera>();
            }
            return instance;
        }
        private set { }
    }
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        vcam = GetComponent<CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        tPlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    private void LateUpdate()
    {
        Vector3 direction = (tPlayer.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("EnvironmentObject"));

        for (int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent();

            }
        }
    }

    public void CheckPlayer()
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
}
