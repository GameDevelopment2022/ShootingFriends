using Fusion;
using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSectionPanel : LobbyPanelBase
{
    [SerializeField] private Button joinRandomRoomBtn;
    [SerializeField] private Button joinRoomBtn;
    [SerializeField] private Button createRoomBtn;

    [SerializeField] private TMP_InputField createRoomInput;
    [SerializeField] private TMP_InputField joinRoomNameInput;

    [Header("Data")] 
    [SerializeField] StringVariable roomName;
    [SerializeField] GameModeData gameModeData;


    [Header("Actions")] 
    [SerializeField] ScriptableEventNoParam StartGameEvent;


    void Start()
    {
        joinRoomBtn.interactable = false;
        createRoomBtn.interactable = false;

        joinRandomRoomBtn.onClick.AddListener(JoinRandomRoom);
        joinRoomBtn.onClick.AddListener(JoinRoom);
        createRoomBtn.onClick.AddListener(CreateRoom);

        joinRoomNameInput.onValueChanged.AddListener((string value) =>
        {
            joinRoomBtn.interactable = value.Length >= 5;
        });

        createRoomInput.onValueChanged.AddListener((string value) =>
        {
            createRoomBtn.interactable = value.Length >= 5;
        });
    }

    private void CreateRoom()
    {
        if (createRoomInput.text.Length < 5)
            return;

        roomName.Value = createRoomInput.text;
        gameModeData.Value = GameMode.Host;

        StartGameEvent?.Raise();
    }

    private void JoinRoom()
    {
        if (joinRoomNameInput.text.Length < 5)
            return;

        roomName.Value = joinRoomNameInput.text;
        gameModeData.Value = GameMode.Client;

        StartGameEvent?.Raise();
    }

    private void JoinRandomRoom()
    {
        roomName.Value = string.Empty;
        gameModeData.Value = GameMode.AutoHostOrClient;
        StartGameEvent?.Raise();
    }
}