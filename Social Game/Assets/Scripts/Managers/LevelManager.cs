using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {get; private set;}

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

    public string getCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void LoadScene(string sceneName)
    {
        CircleWipeFade.Instance.StartFade(() => {
            StartCoroutine(LoadLevel(sceneName));
        });
    }

    private IEnumerator LoadLevel(string sceneToLoad)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        while (!asyncLoadLevel.isDone)
            yield return null;
        
        CircleWipeFade.Instance.EndFade();
        //InputManager.Instance.SpawnPlayers();
    }
    
}
