using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public InputDevice[] playerDevices = new InputDevice[6];
    public int[] charactersSelected = new int[6];
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

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        amountJoined += 1;
        playerDevices[playerInput.playerIndex] = playerInput.devices[0];
    }

    // MainMenu
    public void ReadyPlayer(int playerIndex, int characterSelected)
    {
        amountReady += 1;
        charactersSelected[playerIndex] = characterSelected;

        //bool isReady = InputManager.Instance.ArePlayersReady(amountReady);
        if (amountJoined == amountReady)
        {
            CircleWipeFade.Instance.StartFade(() => {
                LevelManager.Instance.LoadScene("LevelSelection");
                //LevelCanvas.gameObject.SetActive(true);
                //CircleWipeFade.Instance.EndFade();
            });
        }
    }

    //Game Level
    public void CheckFinish(int playersAtFlag)
    {
        bool finish = InputManager.Instance.ArePlayersReady(playersAtFlag);
        if(finish)
        {
            CircleWipeFade.Instance.StartFade(() => {
                LevelManager.Instance.LoadScene("LevelSelection");
            });
        }
    }
}
