using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSectionPanel : LobbyPanelBase
{
    [Header("Middle Section Vars")]
    [SerializeField] private int minRoomNameLength = 2;

    [SerializeField] private Button joinRandomRoomBtn;
    [SerializeField] private Button joinRoomByArgBtn;
    [SerializeField] private Button createRoomBtn;

    [SerializeField] private TMP_InputField joinRoomNameInput;
    [SerializeField] private TMP_InputField createRoomNameInput;

    private NetworkRunnerController _networkRunnerController;


    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);

        _networkRunnerController = GlobalManagers.Instance.networkRunnerController;
        createRoomBtn.onClick.AddListener(() => JoinOrCreateRoom(GameMode.Host, createRoomNameInput.text));
        joinRoomByArgBtn.onClick.AddListener(() => JoinOrCreateRoom(GameMode.Client, joinRoomNameInput.text));

    }
    private void JoinOrCreateRoom(GameMode mode, string field)
    {
        if (field.Length >= minRoomNameLength)
        {
            _networkRunnerController.StartGame(mode, field);
        }
    }

    private void JoinRandomRoom()
    {
        _networkRunnerController.StartGame(GameMode.AutoHostOrClient, string.Empty);
    }

}
