using System.Collections;
using UnityEngine;

public class BindCamera : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(Bind());
    }

    IEnumerator Bind()
    {
        yield return null;
        yield return null;

        Canvas canvas = GetComponent<Canvas>() ?? GetComponentInParent<Canvas>();

        if (canvas == null)
        {
            Debug.LogError($"No Canvas found on {gameObject.name}");
            yield break;
        }

        Debug.Log($"{canvas.name} render mode = {canvas.renderMode}");

        if (canvas.renderMode != RenderMode.ScreenSpaceCamera)
        {
            Debug.Log($"{canvas.name} is not Screen Space - Camera");
            yield break;
        }

        Camera cam = PersistentCamera.Instance;

        if (cam == null)
        {
            Debug.LogError($"Persistent camera missing for {canvas.name}");
            yield break;
        }

        canvas.worldCamera = cam;

        Debug.Log($"Assigned {cam.name} → {canvas.name}");
    }
}
