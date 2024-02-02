using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 5;
    public float levelWidth = 3f;
    public float minY = 5f;
    public float maxY = 8f;

    public float verticalOffset = 10.0f; //Minimum distance between player and the highest platform
    private float nextSpawnY = 0.0f;
    public float horizontalLimit = 3f;
    public float platformSpacing = 2f; // vertical distance between different platform

    //private Vector3 spawnPosition = new Vector3();

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
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-cameraWidth/2, cameraWidth/2);
            Instantiate(platformPrefab, spawnPosition, UnityEngine.Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // 当玩家接近当前最高平台时，生成新平台
            while (player.transform.position.y + verticalOffset > nextSpawnY)
            {
                SpawnPlatform(nextSpawnY);
                nextSpawnY += platformSpacing;
            }
        }
    }

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
            
            Debug.Log($"Camera Bounds:\nTop Left: {cameraTopLeft}\nTop Right: {cameraTopRight}\nBottom Left: {cameraBottomLeft}\nBottom Right: {cameraBottomRight}");
        }
        else
        {
            Debug.LogError("Camera is not orthographic.");
        }
    }

    void SpawnPlatform(float spawnY)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-horizontalLimit, horizontalLimit), spawnY, 0);
        Instantiate(platformPrefab, spawnPosition, UnityEngine.Quaternion.identity);
    }
}
