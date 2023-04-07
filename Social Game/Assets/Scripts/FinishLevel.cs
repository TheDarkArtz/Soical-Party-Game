using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private int _playersAtFlag = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            _playersAtFlag += 1;
            GameManager.Instance.CheckFinish(_playersAtFlag);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            _playersAtFlag -= 1;
        }
    }
}
