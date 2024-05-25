using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Capture2D : MonoBehaviour
{
        public Camera cameraToUse; // 캡처에 사용할 카메라
    public string filePath = "Assets/Screenshots/transparent_screenshot.png"; // 이미지 파일 경로

    void Start()
    {
        if (cameraToUse == null)
            cameraToUse = Camera.main; // 카메라가 지정되지 않았다면 메인 카메라 사용
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면 캡처
        {
            Capture();
        }
    }

    void Capture()
    {
        // Ensure the directory exists
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // 화면의 가로와 세로 크기가 같은 이미지를 캡처하기 위해 가로 크기와 세로 크기를 동일하게 설정
        int imageSize = Mathf.Min(Screen.width, Screen.height);
        RenderTexture renderTexture = new RenderTexture(imageSize, imageSize, 24, RenderTextureFormat.ARGB32);
        cameraToUse.targetTexture = renderTexture;
        Texture2D screenShot = new Texture2D(imageSize, imageSize, TextureFormat.RGBA32, false);
        cameraToUse.clearFlags = CameraClearFlags.SolidColor; // Ensure clear flags are set to solid color
        cameraToUse.backgroundColor = new Color(0, 0, 0, 0); // Set the background color to transparent

        cameraToUse.Render();

        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, imageSize, imageSize), 0, 0);
        screenShot.Apply();

        cameraToUse.targetTexture = null;
        RenderTexture.active = null; // Clean up the render texture
        Destroy(renderTexture);

        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
        Debug.Log($"Screenshot saved to: {filePath}");
    }
}
