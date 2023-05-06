using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    private void Awake() {
        playerInputManager = gameObject.GetComponent<PlayerInputManager>();
    }

    void Start()
    {
        int amountOfPlayers = GameManager.Instance.amountJoined;
        for(int playerIndex = 0; playerIndex < amountOfPlayers; playerIndex++)
        {
            playerInputManager.playerPrefab = GameManager.Instance.characterPrefabs[GameManager.Instance.charactersSelected[playerIndex]];
            PlayerInput playerInput = playerInputManager.JoinPlayer(playerIndex,-1,null,GameManager.Instance.playerDevices[playerIndex]);
        }
    }
}
