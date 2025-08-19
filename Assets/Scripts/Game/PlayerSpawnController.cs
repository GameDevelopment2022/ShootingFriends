using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawnController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef playerPrefab = NetworkPrefabRef.Empty;
    [SerializeField] public List<Transform> playerSpawnPoints = new List<Transform>();


    public override void Spawned()
    {
        if (Runner.IsServer)
        {
            foreach (var player in Runner.ActivePlayers)
            {
                SpawnPlayer(player);
            }
        }
    }

    private void SpawnPlayer(PlayerRef player)
    {
        if (Runner.IsServer)
        {
            var index = player % playerSpawnPoints.Count;
            var spawnPoint = playerSpawnPoints[index].transform.position;


            var playerObj = Runner.Spawn(playerPrefab, spawnPoint, Quaternion.identity, player);

            Runner.SetPlayerObject(player, playerObj);
        }
    }
    
    private void DeSpawnPlayer(PlayerRef player)
    {
        if (Runner.IsServer)
        {
            if (Runner.TryGetPlayerObject(player, out var playerObj))
            {
                Runner.Despawn(playerObj);
            }
            Runner.SetPlayerObject(player, null);
        }
    }


    public void PlayerJoined(PlayerRef player)
    {
        SpawnPlayer(player);
    }

    public void PlayerLeft(PlayerRef player)
    {
        DeSpawnPlayer(player);
    }
}