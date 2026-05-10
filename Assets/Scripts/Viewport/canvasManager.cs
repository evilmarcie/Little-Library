using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasManager : MonoBehaviour
{
     void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Bind();
    }

    void Bind()
    {
        Camera cam = PersistentCamera.Instance;

        if (cam == null)
        {
            Debug.LogWarning("No persistent camera found");
            return;
        }

        var canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        
        foreach (var canvas in canvases)
        {
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                canvas.worldCamera = cam;
                Debug.Log($"Assigned {cam.name} to {canvas.name}");
            }
        }
    }
}

