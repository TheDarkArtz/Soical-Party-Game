using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CircleWipeFade : MonoBehaviour
{
    public static CircleWipeFade Instance {get; private set;}
    [SerializeField] private Material mat;

    private Color _in;
    private Color _out;

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

    private void Start() {
        mat.SetFloat("_Radius",1f);
    }

    public void StartFade(Action callback) => mat.DOFloat(0f,"_Radius",1f).OnComplete(() => callback?.Invoke() );
    public void EndFade() => mat.DOFloat(1f,"_Radius",1f);
}
