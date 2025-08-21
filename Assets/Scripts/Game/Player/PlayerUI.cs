using System.Collections;
using System.Collections.Generic;
using Fusion;
using Obvious.Soap;
using TMPro;
using UnityEngine;

public class PlayerUI : NetworkBehaviour
{
    public TextMeshProUGUI playerNameText;


    [Networked(OnChanged = nameof(OnNickNameChanged))]
    private NetworkString<_8> playerName { get; set; }


    public override void Spawned()
    {
        if (Runner.LocalPlayer == Object.HasInputAuthority)
            RpcSetNickName();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetNickName()
    {
        playerName = UserData.Instance.playerName;
    }

    private static void OnNickNameChanged(Changed<PlayerUI> changed)
    {
        changed.Behaviour.SetPlayerNickName(changed.Behaviour.playerName);
    }

    private void SetPlayerNickName(NetworkString<_8> nickName)
    {
        playerNameText.text = $"{nickName} {Object.InputAuthority.PlayerId}";
    }
}