using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private Camera myCamera;
    private bool takeScreenshot;

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>(); //Camera.main;
    }

    private void OnPostRender()
    {
        if (takeScreenshot)
        {
            takeScreenshot = false;
        
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath +"/CameraScreenshot.png", byteArray);
            Debug.Log("Saved CamereaScreenshot.png");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        } 
    }

    private void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshot = true;
    }

    public void TakeScreenshot_button()
    {
        instance.TakeScreenshot(Screen.width, Screen.height);
    }

}
