using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

public class LobbyLoadingPanel : LobbyPanelBase
{
    [SerializeField] private Button cancelButton;

    [SerializeField] private ScriptableEventNoParam CancelEvent;

    // Start is called before the first frame update
    void Start()
    {
        cancelButton.onClick.AddListener(Cancel);
    }

    private void Cancel()
    {
        CancelEvent?.Raise();
    }
}