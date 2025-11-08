using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkRunnerController : MonoBehaviour, INetworkRunnerCallbacks
{

    public event Action OnStartedRunnerConnection;
    public event Action OnPlayerJoinedSuccessfully;


    [SerializeField] private string gameSceneName;
    [SerializeField] private string lobbySceneName;
    [SerializeField] private NetworkRunner networkRunnerPrefab;


    private NetworkRunner networkRunnerInstace;



    public async void StartGame(GameMode gameMode, string roomName)
    {
        OnStartedRunnerConnection?.Invoke();

        if (networkRunnerInstace == null)
        {
            networkRunnerInstace = Instantiate(networkRunnerPrefab);
        }

        networkRunnerInstace.AddCallbacks(this);

        //networkRunnerInstace.ProvideInput = true;


        var startGameArgs = new StartGameArgs()
        {
            GameMode = gameMode,
            SessionName = roomName,
            PlayerCount = 4,
            SceneManager = networkRunnerInstace.GetComponent<INetworkSceneManager>()
        };


        var result = await networkRunnerInstace.StartGame(startGameArgs);

        if (result.Ok)
        {
            networkRunnerInstace.SetActiveScene(gameSceneName);
        }
        else
        {
            Debug.Log($"Failed to start game: {result.ShutdownReason}");
        }
    }



    public void Shutdown()
    {
        networkRunnerInstace.Shutdown();
    }


    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"[Fusion] Player Joined: {player}");
        OnPlayerJoinedSuccessfully?.Invoke();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"[Fusion] Player Left: {player}");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        Debug.Log("[Fusion] OnInput called");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        Debug.Log($"[Fusion] Input Missing for Player: {player}");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log($"[Fusion] Runner Shutdown: {shutdownReason}");

        SceneManager.LoadScene(lobbySceneName);
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("[Fusion] Connected to Server");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("[Fusion] Disconnected from Server");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("[Fusion] Connect Request received");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log($"[Fusion] Connect Failed: {reason}, Address: {remoteAddress}");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("[Fusion] User Simulation Message received");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log($"[Fusion] Session List Updated. Count: {sessionList.Count}");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("[Fusion] Custom Authentication Response received");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("[Fusion] Host Migration occurred");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log($"[Fusion] Reliable Data Received from Player: {player}, Size: {data.Count} bytes");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("[Fusion] Scene Load Done");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("[Fusion] Scene Load Start");
    }
}
