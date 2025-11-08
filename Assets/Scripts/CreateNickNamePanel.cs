using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNickNamePanel : LobbyPanelBase
{
    [Header("Nick Name Vars")]
    [SerializeField] private int minNickNameLength;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button createNickNameButton;



    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);
        createNickNameButton.interactable = false;
        createNickNameButton.onClick.AddListener(OnClickCreateNickName);
        _inputField.onValueChanged.AddListener(OnValueChange);
    }
    private void OnValueChange(string arg0)
    {
        createNickNameButton.interactable = arg0.Length >= minNickNameLength;
    }
    private void OnClickCreateNickName()
    {
        var nickName = _inputField.text;

        if (nickName.Length >= minNickNameLength)
        {
            HidePanel();
            _lobbyUIManager.ShowPanel(LobbyPanelType.MiddleSectionPanel);
        }
    }


}
