using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLevelSelector : MonoBehaviour
{
    private GameObject _levelContainer;

    private int _selectedLevel = 0;
    private int _levelCount;
    private bool _ready = false;
    private string _levelName = "One";

    private void Awake() {
        _levelContainer = GameObject.FindGameObjectWithTag("LevelContainer");
        _levelCount = _levelContainer.transform.childCount;
    }

    private void Start() {
        Transform Parent = _levelContainer.transform.GetChild(_selectedLevel).GetChild(1);
        transform.SetParent(Parent);
    }

    public void SelectCharacter(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _ready == false)
        {
            float dir = ctx.ReadValue<float>();
            _selectedLevel += (int) dir;

            Transform Parent = _levelContainer.transform.GetChild(_selectedLevel % _levelCount);
            _levelName = Parent.name;

            transform.SetParent(Parent.GetChild(1));
        }
    }

    public void Ready(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _ready == false)
        {
            _ready = true;
            GameManager.Instance.LevelSelected(_levelName);
        }
    }

}
