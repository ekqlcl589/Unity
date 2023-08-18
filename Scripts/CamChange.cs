using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamChange : MonoBehaviour
{
    public CinemachineVirtualCamera followCam = null;

    public void onChangePriority()
    {
        if (followCam.Priority == 12)
            followCam.Priority = 9;
        else
            followCam.Priority = 12;

    }
}
