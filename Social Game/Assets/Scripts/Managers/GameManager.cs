using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public InputDevice[] playerDevices = new InputDevice[6];
    public int[] charactersSelected = new int[6];
    public GameObject[] characterPrefabs = new GameObject[6];

    public int amountJoined = 0;
    public int amountReady = 0;

    //[SerializeField] public Canvas LevelCanvas;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // MainMenu
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        amountJoined += 1;
        playerDevices[playerInput.playerIndex] = playerInput.devices[0];
    }
    
    public void ReadyPlayer(int playerIndex, int characterSelected)
    {
        amountReady += 1;
        charactersSelected[playerIndex] = characterSelected;

        if (amountReady == amountJoined)
        {
            CircleWipeFade.Instance.StartFade(() => {
                LevelManager.Instance.LoadScene("LevelSelection");
            });
        }
    }

    //Level Selection
    public void LevelSelected(string levelName)
    {
        // Voting Check

        CircleWipeFade.Instance.StartFade(() => {
            LevelManager.Instance.LoadScene(levelName);
        });
    }

    //Game Level
    public void CheckFinish(int playersAtFlag)
    {
        if(playersAtFlag == amountJoined)
        {
            CircleWipeFade.Instance.StartFade(() => {
                LevelManager.Instance.LoadScene("LevelSelection");
            });
        }
    }
}
