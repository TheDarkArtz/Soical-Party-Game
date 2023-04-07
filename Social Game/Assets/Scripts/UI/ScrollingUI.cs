using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingUI : MonoBehaviour
{
    private RawImage _img;
    [SerializeField] private Vector2 speed;

    private void Awake() {
        _img = gameObject.GetComponent<RawImage>();
    }

    private void Update() {
        _img.uvRect = new Rect(_img.uvRect.position + speed * Time.unscaledDeltaTime, _img.uvRect.size);
    }
}
