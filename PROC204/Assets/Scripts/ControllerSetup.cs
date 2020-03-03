using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class ControllerSetup : MonoBehaviour
{
    [SerializeField] Text p1Status;
    [SerializeField] Text p2Status;
    [SerializeField] Button startButton;

    PlayerInputManager inputManager;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        SetStatus("Cardinal", ControllerStatus.Default);
        SetStatus("Prentice", ControllerStatus.Default);
    }

    private void OnEnable()
    {
        inputManager.onPlayerJoined += AddPlayer;
        inputManager.onPlayerLeft += RemovePlayer;
    }

    private void OnDisable()
    {
        inputManager.onPlayerJoined -= AddPlayer;
        inputManager.onPlayerLeft -= RemovePlayer;
    }

    private void AddPlayer(PlayerInput input)
    {
        CheckStart();

        input.onDeviceLost += ControllerLost;
        input.onDeviceRegained += ControllerReconnected;
    }

    private void CheckStart()
    {
        if (inputManager.playerCount >= 2) startButton.interactable = true;
        else startButton.interactable = false;
    }

    private void RemovePlayer(PlayerInput input)
    {
        input.onDeviceLost -= ControllerLost;
        input.onDeviceRegained -= ControllerReconnected;
    }

    private void ControllerReconnected(PlayerInput input)
    {
        SetStatus(input.defaultActionMap, ControllerStatus.Connected);
    }

    private void ControllerLost(PlayerInput input)
    {
        SetStatus(input.defaultActionMap, ControllerStatus.Lost);
    }

    public void SetStatus(string characterName, ControllerStatus status)
    {
        Text output;

        if (characterName == "Cardinal") output = p1Status;
        else output = p2Status;

        switch (status)
        {
            case ControllerStatus.Connected:
                output.text = "Controller connected";
                output.color = Color.green;
                break;
            case ControllerStatus.Lost:
                output.text = "Controller lost";
                output.color = Color.red;
                break;
            default:
                output.text = "Press [button south] to join!";
                output.color = Color.white;
                break;
        }
    }    
}

public enum ControllerStatus
{
    Connected,
    Lost,
    Default
}
