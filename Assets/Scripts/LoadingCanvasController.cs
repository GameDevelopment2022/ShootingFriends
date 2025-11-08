using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvasController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button cancelBtn;

    [SerializeField] private string InClip = "In";
    [SerializeField] private string OutClip = "Out";


    private NetworkRunnerController _networkRunnerController;

    private void Start()
    {
        _networkRunnerController = GlobalManagers.Instance.networkRunnerController;

        _networkRunnerController.OnStartedRunnerConnection += OnStartedRunnerConnection;
        _networkRunnerController.OnPlayerJoinedSuccessfully += OnPlayerJoinedSuccessfully;

        cancelBtn.onClick.AddListener(CancelRequest);

        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _networkRunnerController.OnStartedRunnerConnection -= OnStartedRunnerConnection;
        _networkRunnerController.OnPlayerJoinedSuccessfully -= OnPlayerJoinedSuccessfully;
    }
    private void OnPlayerJoinedSuccessfully()
    {
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, _animator, OutClip, false));
    }
    private void OnStartedRunnerConnection()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, _animator, InClip));
    }
    private void CancelRequest()
    {
        _networkRunnerController.Shutdown();
    }
}
