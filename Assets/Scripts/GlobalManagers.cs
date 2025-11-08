using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManagers : MonoBehaviour
{
    public static GlobalManagers Instance { get; private set; }
    [SerializeField] private GameObject parentObj;
    [field: SerializeField] public NetworkRunnerController networkRunnerController { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(parentObj);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
