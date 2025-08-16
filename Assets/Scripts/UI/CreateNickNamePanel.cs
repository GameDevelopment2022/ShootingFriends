using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNickNamePanel : LobbyPanelBase
{
    [Header("Nick Name Variables")] public TMP_InputField nickNameInput;
    public Button createNickNameButton;

    public int minCharactersForNickName;


    void Start()
    {
        createNickNameButton.interactable = false;

        createNickNameButton.onClick.AddListener(OnCreateNickNameClick);
        nickNameInput.onValueChanged.AddListener(ValidateNickNameInput);
    }

    private void ValidateNickNameInput(string arg0)
    {
        createNickNameButton.interactable = arg0.Length >= minCharactersForNickName;
    }

    private void OnCreateNickNameClick()
    {
        if (nickNameInput.text.Length < minCharactersForNickName)
            return;

        LobbyPanelChangeEvent?.Raise(PanelType.MiddleSectionPanel);
        //Change Panel
    }
}