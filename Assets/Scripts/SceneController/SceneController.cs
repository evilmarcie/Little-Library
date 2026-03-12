using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public LoadingOverlay loadingOverlay;

    private Dictionary <string, string> loadedSceneBySlot = new();
    private bool isBusy = false;

    //public string SceneName { get; private set; }

    public SceneTransitionPlan NewTransition()
    {
        return new SceneTransitionPlan();
    }

    private Coroutine ExecutePlan(SceneTransitionPlan plan)
    {
        if (isBusy)
        {
            Debug.LogWarning("scene change already in progress");
            return null;
        }
        isBusy = true;
        return StartCoroutine(ChangeSceneRoutine(plan));
    }

    private IEnumerator ChangeSceneRoutine(SceneTransitionPlan plan)
    {
        if (plan.Overlay)
        {
            yield return loadingOverlay.FadeInBlack();
            yield return new WaitForSeconds(0.5f);
        }
        foreach (var slotKey in plan.ScenesToUnload)
        {
            yield return UnloadSceneRoutine(slotKey);
        }
        if (plan.ClearUnusedAssets) yield return CleanUnusedAssetsRoutine();

        foreach (var kvp in plan.ScenesToLoad)
        {
            if (loadedSceneBySlot.ContainsKey(kvp.Key))
            {
                yield return UnloadSceneRoutine(kvp.Key);
            }
            yield return LoadAdditiveRoutine(kvp.Key, kvp.Value, plan.ActiveSceneName == kvp.Value);
        }
        if (plan.Overlay)
        {
            yield return loadingOverlay.FadeOutBlack();
        }
        isBusy = false;

        //if (plan.SwapScreen)
        //{

           // Scene scene = SceneManager.GetSceneByName(SceneName);
            //yield return SceneManager.SetActiveScene(scene);
        //}
    }

    private IEnumerator LoadAdditiveRoutine(string slotKey, string sceneName, bool SetActive)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (loadOp == null) yield break;
        loadOp.allowSceneActivation = false;
        while (loadOp.progress < 0.9f)
        {
            yield return null;
        }

        loadOp.allowSceneActivation = true;
        while (!loadOp.isDone)
        {
            yield return null;
        }

        if (SetActive)
        {
            Scene newScene = SceneManager.GetSceneByName(sceneName);
            if (newScene.IsValid() && newScene.isLoaded)
            {
                SceneManager.SetActiveScene(newScene);
            }
        }

        loadedSceneBySlot[slotKey] = sceneName;
    }

    private IEnumerator UnloadSceneRoutine(string slotkey)
    {
        if (!loadedSceneBySlot.TryGetValue(slotkey, out string sceneName)) yield break;
        if (string.IsNullOrEmpty(sceneName)) yield break;
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneName);
        if (unloadOp != null)
        {
            while (!unloadOp.isDone)
            {
                yield return null;
            }
        }
        loadedSceneBySlot.Remove(slotkey);
    }

    private IEnumerator CleanUnusedAssetsRoutine()
    {
        AsyncOperation cleanupOp = Resources.UnloadUnusedAssets();
        while (!cleanupOp.isDone)
        {
            yield return null;
        }
    }

    public class SceneTransitionPlan
    {
        public Dictionary <string, string> ScenesToLoad {get;} = new();
        public List <string> ScenesToUnload {get;} = new();
        public string ActiveSceneName {get; private set;} = "";
        public bool ClearUnusedAssets {get; private set;} = false;
        public bool Overlay {get; private set;} = false;
        
        //public bool SwapScreen = false;
        public SceneTransitionPlan Load(string slotKey, string SceneName, bool SetActive = false)
        {
            ScenesToLoad[slotKey] = SceneName;
            if (SetActive) ActiveSceneName = SceneName;
            return this;
        }
        public SceneTransitionPlan Unload(string slotKey)
        {
            ScenesToUnload.Add(slotKey);
            return this;
        }
        public SceneTransitionPlan WithOverlay()
        {
            Overlay = true;
            return this;
        }
        public SceneTransitionPlan SwapScreen(string SceneName)
        {
            Scene scene = SceneManager.GetSceneByName(SceneName);
            SceneManager.SetActiveScene(scene);
            return this;
        }
        public SceneTransitionPlan WithClearUnusedAssets()
        {
            ClearUnusedAssets = true;
            return this;
        }
        public Coroutine Perform()
        {
            return SceneController.Instance.ExecutePlan(this);
        }
    }

}
