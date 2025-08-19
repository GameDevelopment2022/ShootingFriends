using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Obvious.Soap;
using UnityEngine;

public class NetworkController : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner networkRunnerPrefab;
    [SerializeField] private int maxPlayersPerRoom;
    [SerializeField] private string trialRoom;
    [SerializeField] private string lobbyRoom;

    [Header("Data")] [SerializeField] StringVariable roomName;
    [SerializeField] GameModeData gameModeData;

    [Header("Receptions")] [SerializeField]
    ScriptableEventNoParam StartGameEvent;

    [SerializeField] ScriptableEventNoParam CancelGameEvent;
    [SerializeField] protected LobbyPanelChangeEvent LobbyPanelChangeEvent;

    private NetworkRunner networkRunnerInstance;

    private void Start()
    {
        StartGameEvent.OnRaised += StartGame;
        CancelGameEvent.OnRaised += CancelGame;
    }

    private void OnDestroy()
    {
        StartGameEvent.OnRaised -= StartGame;
        CancelGameEvent.OnRaised -= CancelGame;
    }

    private void StartGame()
    {
        StartGame(gameModeData.Value, roomName.Value);
    }

    private void CancelGame()
    {
        networkRunnerInstance.Shutdown();
    }


    public async void StartGame(GameMode mode, string roomName)
    {
        if (networkRunnerInstance == null)
        {
            networkRunnerInstance = Instantiate(networkRunnerPrefab);
        }


        networkRunnerInstance.AddCallbacks(this);

        networkRunnerInstance.ProvideInput = true;

        var startGameArgs = new StartGameArgs
        {
            GameMode = mode,
            SessionName = roomName,
            PlayerCount = maxPlayersPerRoom,
            SceneManager = networkRunnerInstance.GetComponent<INetworkSceneManager>()
        };

        LobbyPanelChangeEvent.Raise(PanelType.LoadingPanel);

        var result = await networkRunnerInstance.StartGame(startGameArgs);

        if (result.Ok)
        {
            LobbyPanelChangeEvent.Raise(PanelType.None);
            networkRunnerInstance.SetActiveScene(trialRoom);
        }
        else
        {
            Debug.LogError($"Failed to start game: Reason {result.ShutdownReason}");
        }
    }

    void Update()
    {
        // Optional: uncomment if you want frame-by-frame logging
        // Debug.Log("[NetworkController] Update()");
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"[NetworkController] Player Joined: {player}");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"[NetworkController] Player Left: {player}");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
//        Debug.Log("[NetworkController] OnInput called.");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        Debug.Log($"[NetworkController] OnInputMissing for Player: {player}");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        LobbyPanelChangeEvent.Raise(PanelType.MiddleSectionPanel);
        Debug.Log($"[NetworkController] OnShutdown - Reason: {shutdownReason}");
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("[NetworkController] Connected to Server.");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("[NetworkController] Disconnected from Server.");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("[NetworkController] ConnectRequest received.");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log($"[NetworkController] ConnectFailed - Address: {remoteAddress} | Reason: {reason}");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("[NetworkController] UserSimulationMessage received.");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log($"[NetworkController] SessionListUpdated - Found {sessionList.Count} sessions.");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("[NetworkController] CustomAuthenticationResponse received.");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("[NetworkController] HostMigration triggered.");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log($"[NetworkController] ReliableDataReceived from Player {player} - Size: {data.Count} bytes.");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("[NetworkController] SceneLoadDone.");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("[NetworkController] SceneLoadStart.");
    }
}