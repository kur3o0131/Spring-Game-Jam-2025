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
        // wait one frame so camera finishes rendering
        yield return new WaitForEndOfFrame();

        SaveRenderTextureToPNG();
    }

    void SaveRenderTextureToPNG()
    {
        if (renderTexture == null)
        {
            Debug.LogError("RenderTexture is not assigned.");
            return;
        }

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        int width = renderTexture.width;
        int height = renderTexture.height;

        Debug.Log($"Capturing render texture at {width}x{height}");

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // 🔆 apply gamma correction for Linear color space
        if (QualitySettings.activeColorSpace == ColorSpace.Linear)
        {
            Color[] pixels = tex.GetPixels();
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i].r = Mathf.Pow(pixels[i].r, 1f / 2.2f);
                pixels[i].g = Mathf.Pow(pixels[i].g, 1f / 2.2f);
                pixels[i].b = Mathf.Pow(pixels[i].b, 1f / 2.2f);
            }
            tex.SetPixels(pixels);
            tex.Apply();
        }

        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(outputPath, bytes);
        Debug.Log("PNG saved to: " + outputPath);

        RenderTexture.active = currentRT;

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
