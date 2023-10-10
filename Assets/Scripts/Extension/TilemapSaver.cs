using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Collections;

public class TilemapSaver : MonoBehaviour
{
    public Tilemap tilemapToSave;

    public void Start(){
        SaveTilemapAsPNG("D:/Nghia/Unity/Project/LevelDesign/Assets/Exported Tilemaps/Level1.png");
    }
    public void SaveTilemapAsPNG(string savePath)
    {
        // Capture a screenshot of the Tilemap
        ScreenCapture.CaptureScreenshot(savePath);

        // Wait for a frame to ensure the screenshot is taken
        StartCoroutine(WaitForScreenshotToSave(savePath));
    }

    private IEnumerator WaitForScreenshotToSave(string savePath)
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second (you can adjust this as needed)

        // Check if the screenshot file exists
        if (File.Exists(savePath))
        {
            Debug.Log("Tilemap screenshot saved as " + savePath);
        }
        else
        {
            Debug.LogError("Failed to save the Tilemap screenshot.");
        }
    }
}
