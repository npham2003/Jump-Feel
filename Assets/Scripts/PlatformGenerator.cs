using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using UnityEditor.SearchService;
using Unity.VisualScripting;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject initPlatform;
    public int numberOfPlatforms = 5;

    public float verticalOffset = 20.0f; //Minimum distance between player and the highest platform
    private float nextSpawnY = -2f;
    public float horizontalLimit = 3f;
    public float platformSpacing = 10f; // vertical distance between different platform

    //private Vector3 spawnPosition = new Vector3();

    private float cameraHeight;
    private float cameraWidth;
    private Vector2 cameraBottomLeft;
    private Vector2 cameraTopRight;
    private Vector2 cameraTopLeft;
    private Vector2 cameraBottomRight;
    // Start is called before the first frame update
    private List<GameObject> platforms = new List<GameObject>();
    private float lastX = 0;
    

    void Start()
    {  
        platforms.Add(initPlatform);
        CalculateCameraBounds();
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatform(nextSpawnY);
            nextSpawnY += platformSpacing;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            while (player.transform.position.y + verticalOffset > platforms[platforms.Count-5].transform.position.y)
            {
                SpawnPlatform(platforms[platforms.Count-1].transform.position.y + platformSpacing);
            }
        }
        for(int i = platforms.Count - 1; i >= 0; i--)
        {
            if(IsObjectBelowCamera(platforms[i].transform))
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
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
        float spawnX = Random.Range(lastX-horizontalLimit, lastX+horizontalLimit);
        if(lastX-horizontalLimit < -cameraWidth/2)
        {
            spawnX = Random.Range(-cameraWidth/2, -cameraWidth/2 + 2*horizontalLimit);
        }
        else if(lastX+horizontalLimit > cameraWidth/2)
        {
            spawnX = Random.Range(cameraWidth/2-2*horizontalLimit, cameraWidth/2);
        }
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        platforms.Add(Instantiate(platformPrefab, spawnPosition, UnityEngine.Quaternion.identity));
        lastX = spawnPosition.x;
    }

    bool IsObjectBelowCamera(Transform objTransform)
    {
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        return objTransform.position.y < cameraBottom;
    }

    public void scrollDown(float distance)
    {
        for(int i = platforms.Count - 1; i >= 0; i--)
        {
            platforms[i].transform.Translate(0, -distance,0);
        }
    }
}
