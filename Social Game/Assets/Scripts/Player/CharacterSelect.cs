using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    private PlayerInput playerInput;

    private TMP_Text _readyText;
    private Image _spriteImage;
    public Sprite[] _characterSprites;
    
    private int _selectedCharacter = 0;
    private bool _ready = false;

    private void Awake() {
        _readyText = GetComponentInChildren<TMP_Text>();
        _spriteImage = gameObject.GetComponent<Image>();

        playerInput = gameObject.GetComponent<PlayerInput>();
    }

    private void Start() {
        var scheme = playerInput.currentControlScheme;
        _spriteImage.sprite = _characterSprites[_selectedCharacter];

        if(scheme == "Keyboard")
        {
            _readyText.text = "Press Space to Ready!";
        }
        else
        {
            _readyText.text = "Press [key] to Ready!";
        }
    }

    public void SelectCharacter(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _ready == false)
        {
            float dir = ctx.ReadValue<float>();
            _selectedCharacter += (int) dir;
            _spriteImage.sprite = _characterSprites[_selectedCharacter % _characterSprites.Length];
        }
    }

    public void Ready(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _ready == false)
        {
            _ready = true;
            _readyText.text = "Ready!";
            GameManager.Instance.ReadyPlayer(playerInput.playerIndex, _selectedCharacter);
        }
    }
}
