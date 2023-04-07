using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterParenter : MonoBehaviour
{
    public void OnPlayerAdded(PlayerInput playerInput)
    {
        playerInput.gameObject.transform.SetParent(gameObject.transform, false);
    }
}
