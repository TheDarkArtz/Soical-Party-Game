using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance {get; private set;}
    private PlayerInputManager playerInputManager;

    public GameObject _gameCharacter;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            playerInputManager = gameObject.GetComponent<PlayerInputManager>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if(LevelManager.Instance.getCurrentScene() == "MainMenu")
        {
            GameObject container = GameObject.FindGameObjectWithTag("PlayerSelectionContainer");
            playerInput.gameObject.transform.SetParent(container.transform, false);
        }
        else
        {
            playerInput.gameObject.GetComponent<PlayerController>().playerIndex = playerInput.playerIndex;
        }
    }
    public void OnPlayerLeft(PlayerInput playerInput) {}

    public void SpawnPlayers(PlayerInput playerInput)
    {
        playerInputManager.playerPrefab = _gameCharacter;

        playerInputManager.JoinPlayer(1,-1,playerInput.currentControlScheme);
    }

    public bool ArePlayersReady(int readyCount)
    {
        if (readyCount > 0 && readyCount == playerInputManager.playerCount)
        {
            playerInputManager.DisableJoining();
            return true;
        }
        else
            return false;
    }
}
