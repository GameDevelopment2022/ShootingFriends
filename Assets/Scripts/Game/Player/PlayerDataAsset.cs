using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataAsset", menuName = "Anwar/Player Data Asset")]
public class PlayerDataAsset : ScriptableObject
{
    [SerializeField] private PlayerData playerData; // This is your struct

    public PlayerData Data => playerData;

    public void UpdateInput(float horizontal, InputButtons button)
    {
        playerData.HorizontalInput = horizontal;
        playerData.NetworkButtons.Set(button, Input.GetKey(KeyCode.Space));
    }
}