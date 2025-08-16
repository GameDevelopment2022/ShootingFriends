using UnityEngine;

public class LobbyPanelBase : MonoBehaviour
{
    [Header("Lobby Panel Base Variables")] public PanelType panelType;
    public Animator panelAnimator;

    public string InAnim = "In";
    public string OutAnim = "Out";

    [Header("Actions")] 
    [SerializeField] protected LobbyPanelChangeEvent LobbyPanelChangeEvent;
   

    public void TurnOnPanel()
    {
        panelAnimator.Play(InAnim);
    }

    public void TurnOffPanel()
    {
        panelAnimator.Play(InAnim);
        StartCoroutine(Utils.PerformActionAfterCertainTime(panelAnimator.GetCurrentAnimatorClipInfo(0).Length,
            () => { gameObject.SetActive(false); }));
    }
}