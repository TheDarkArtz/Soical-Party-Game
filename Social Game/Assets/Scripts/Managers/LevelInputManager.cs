using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;

public class LevelInputManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    public GameObject prefab;

    private void Awake() {
        playerInputManager = gameObject.GetComponent<PlayerInputManager>();
    }

    void Start()
    {
        playerInputManager.playerPrefab = prefab;

        int amountOfPlayers = GameManager.Instance.amountJoined;
        for(int i = 0; i < amountOfPlayers; i++)
        {
            PlayerInput playerInput = playerInputManager.JoinPlayer(i,-1,null,GameManager.Instance.playerDevices[i]);
            TMP_Text text = playerInput.GetComponentInChildren<TMP_Text>();
            text.text = "P" + (i + 1);
        }
    }
}
