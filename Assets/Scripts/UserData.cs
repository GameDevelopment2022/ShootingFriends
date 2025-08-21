using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class UserData : NetworkBehaviour
{

    public string playerName;
    
    
    
    
    
    public static UserData Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    
}
