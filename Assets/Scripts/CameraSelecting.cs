using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelecting : NetworkBehaviour
{
    CinemachineFreeLook cinemachinefreelook;
    private void Start()
    {
        cinemachinefreelook = GetComponentInChildren<CinemachineFreeLook>();
        if (isOwned)
            cinemachinefreelook.Priority = 10;
        else
            cinemachinefreelook.Priority = 0;

    }
}
