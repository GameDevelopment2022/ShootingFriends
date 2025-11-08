using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LoadingCanvasController _loadingCanvasControllerPrefab;
    
    [SerializeField] private LobbyPanelBase[] _lobbyPanels;



    private void Start()
    {
        foreach (var lobbyPanel in _lobbyPanels)
        {
            lobbyPanel.InitPanel(this);
        }

        Instantiate(_loadingCanvasControllerPrefab);
    }

    public void ShowPanel(LobbyPanelBase.LobbyPanelType lobbyPanelType)
    {
        foreach (var lobbyPanel in _lobbyPanels)
        {
            if (lobbyPanel._lobbyPanelType == lobbyPanelType)
            {
                lobbyPanel.ShowPanel();
                break;
            }
        }
    }

}
