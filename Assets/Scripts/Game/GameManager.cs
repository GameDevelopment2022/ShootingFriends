using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private Camera mainCam;


    public override void Spawned()
    {
        mainCam.gameObject.SetActive(false);
    }
}