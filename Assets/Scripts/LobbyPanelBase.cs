using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanelBase : MonoBehaviour
{

    [field: SerializeField, Header("Base Vars")]
    public LobbyPanelType _lobbyPanelType { get; private set; }
    [SerializeField] public Animator _animator;


    [SerializeField] private string In_Clip;
    [SerializeField] private string Out_Clip;


    protected LobbyUIManager _lobbyUIManager;

    public enum LobbyPanelType
    {
        None,
        CreateNickNamePanel,
        MiddleSectionPanel
    }

    public virtual void InitPanel(LobbyUIManager uiManager)
    {
        _lobbyUIManager = uiManager;
    }

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
        _animator.Play(In_Clip);
    }

    protected void HidePanel()
    {
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, _animator, Out_Clip, false));
    }
}
