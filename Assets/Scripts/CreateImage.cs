using UnityEngine;
using UnityEngine.UI;

public class CreateImage : MonoBehaviour
{
    public Camera drawCamera;
    public RenderTexture renderTexture;
    public RawImage drawnImage;
    public int textureWidth = 1024;
    public int textureHeight = 1024;
    public Color clearColor = Color.black;
    
    [ContextMenu("Create Drawing")]
    public void CreateDrawing()
    {
        // Tạo RenderTexture tạm thời
        RenderTexture tempRT = RenderTexture.GetTemporary(textureWidth, textureHeight, 24);
        drawCamera.targetTexture = tempRT;
        drawCamera.Render();

        // Tạo Texture2D từ RenderTexture
        RenderTexture.active = tempRT;
        Texture2D tex = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
        tex.Apply();
        
        // Xử lý Texture2D để bỏ qua màu đen
        Color[] pixels = tex.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i] == clearColor)
            {
                pixels[i] = new Color(0, 0, 0, 0); // Đặt pixel trong suốt
            }
        }
        tex.SetPixels(pixels);
        tex.Apply();

        // Gán Texture2D cho RawImage
        drawnImage.texture = tex;

        // Giải phóng tài nguyên
        drawCamera.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(tempRT);
    }
    
}
