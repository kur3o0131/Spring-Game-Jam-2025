using UnityEngine;

public class ForceRenderTexture : MonoBehaviour
{
    public RenderTexture renderTexture;

    void Awake()
    {
        if (renderTexture != null)
        {
            Camera cam = GetComponent<Camera>();
            if (cam != null)
            {
                cam.targetTexture = renderTexture;
                Debug.Log("Assigned targetTexture at runtime.");
            }
        }
    }
}
