using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader instance;

    [SerializeField] private CanvasGroup sceneLoaderCanvasGroup;
    private string loadSceneName;


    private void Init() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Awake() {
        Init();
    }

    public void LoadScene(string sceneName) {
        sceneLoaderCanvasGroup.gameObject.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        this.loadSceneName = sceneName;
        StartCoroutine(LoadingProgress(this.loadSceneName));
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode) {
        if (scene.name == this.loadSceneName) {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    private void OnDestroy() {  // Object destroy -> deligate cut
        SceneManager.sceneLoaded -= LoadSceneEnd;
    }

    IEnumerator LoadingProgress(string sceneName) {
        yield return StartCoroutine(Fade(true));
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;   // Scene loaded -> Wait 

        while(!asyncOp.isDone) {    // scene load complite X
            yield return null;

            if (asyncOp.progress >= 0.9f) { // 90% load
                yield return new WaitForSeconds(0.5f);
                asyncOp.allowSceneActivation = true;
                yield break;
            }
        }
    }

    IEnumerator Fade(bool isFadeIn) {
        float timer = 0;

        while(timer <= 1) {
            yield return null;

            timer += Time.unscaledDeltaTime * 2f;
            this.sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }

        if (!isFadeIn) {
            sceneLoaderCanvasGroup.gameObject.SetActive(false);        
        }
    }
}