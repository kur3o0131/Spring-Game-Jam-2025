using UnityEngine;
using System.IO;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RenderToPNG : MonoBehaviour
{
    public RenderTexture renderTexture;
    public string outputPath = "Assets/RenderedTile.png";

    private void Start()
    {
        StartCoroutine(CaptureAfterFrame());
    }

    IEnumerator CaptureAfterFrame()
    {
        // wait one frame so camera has time to render
        yield return new WaitForEndOfFrame();

        SaveRenderTextureToPNG();
    }

    void SaveRenderTextureToPNG()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(outputPath, bytes);
        Debug.Log("PNG saved to: " + outputPath);

        RenderTexture.active = currentRT;

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
