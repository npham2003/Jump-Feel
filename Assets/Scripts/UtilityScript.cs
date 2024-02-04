using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityScript : MonoBehaviour
{


    private float cameraHeight;
    private float cameraWidth;
    private Vector2 cameraBottomLeft;
    private Vector2 cameraTopRight;
    private Vector2 cameraTopLeft;
    private Vector2 cameraBottomRight;

    // Start is called before the first frame update
    void Start()
    {
        CalculateCameraBounds();
    }

    public float CameraHeight => cameraHeight;
    public float CameraWidth { get {  return cameraWidth; } }
    public Vector2 CameraBottomLeft { get {  return cameraBottomLeft; } }
    public Vector2 CameraBottomRight { get {  return cameraBottomRight; } }
    public Vector2 CameraTopLeft { get { return cameraTopLeft; } }
    public Vector2 CameraTopRight { get { return cameraTopRight; } }

    // Calculates the bounds of the camera view in world space.
    // Useful for positioning objects within the camera's view.
    void CalculateCameraBounds()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera is not assigned.");
            return;
        }

        if (cam.orthographic)
        {
            cameraHeight = cam.orthographicSize * 2;
            cameraWidth = cameraHeight * cam.aspect;

            cameraBottomLeft = new Vector2(cam.transform.position.x - cameraWidth / 2, cam.transform.position.y - cam.orthographicSize);
            cameraTopRight = new Vector2(cam.transform.position.x + cameraWidth / 2, cam.transform.position.y + cam.orthographicSize);

            cameraTopLeft = new Vector2(cameraBottomLeft.x, cameraTopRight.y);
            cameraBottomRight = new Vector2(cameraTopRight.x, cameraBottomLeft.y);

            Debug.Log($"Camera Bounds:\nTop Left: {cameraTopLeft}\nTop Right: {cameraTopRight}\nBottom Left: {cameraBottomLeft}\nBottom Right: {cameraBottomRight}\n Height: {cameraHeight}\n Width: {cameraWidth}");
        }
        else
        {
            Debug.LogError("Camera is not orthographic.");
        }
    }

    public bool IsObjectBelowCamera(Transform objTransform)
    {
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        return objTransform.position.y < cameraBottom - 10f;
    }



}
