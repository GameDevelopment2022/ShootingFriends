using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [Header("UI Panels")] [SerializeField] private List<LobbyPanelBase> lobbyPanels;

    [Header("Receptors for")] [SerializeField]
    private LobbyPanelChangeEvent LobbyPanelChangeEvent;

    private void Start()
    {
        LobbyPanelChangeEvent.OnRaised += LobbyPanelChangeEventOnOnRaised;
    }

    private void OnDestroy()
    {
        LobbyPanelChangeEvent.OnRaised -= LobbyPanelChangeEventOnOnRaised;
    }

    private void LobbyPanelChangeEventOnOnRaised(PanelType type)
    {
        foreach (var panel in lobbyPanels)
        {
            if (panel.panelType == type)
            {
                panel.gameObject.SetActive(true);
                panel.TurnOnPanel();
            }
            else if (panel.gameObject.activeInHierarchy == true)
            {
                panel.TurnOffPanel();
            }
        }
    }
}