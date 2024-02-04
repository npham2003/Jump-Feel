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

    public float platformSpeed;

    //private Vector3 spawnPosition = new Vector3();

    private float cameraHeight;
    private float cameraWidth;
    private Vector2 cameraBottomLeft;
    private Vector2 cameraTopRight;
    private Vector2 cameraTopLeft;
    private Vector2 cameraBottomRight;

    private UtilityScript utilityScript;

    // Start is called before the first frame update
    private List<GameObject> platforms = new List<GameObject>();
    private float lastX = 0;

    private void Awake()
    {

    }

    void Start()
    {

        if (utilityScript == null)
        {
            utilityScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UtilityScript>();
            if (utilityScript == null)
            {
                Debug.LogWarning("script still null");
            }
        }
        cameraHeight = utilityScript.CameraHeight;
        cameraWidth = utilityScript.CameraWidth;
        cameraBottomLeft = utilityScript.CameraBottomLeft;
        cameraBottomRight = utilityScript.CameraBottomRight;
        cameraTopLeft = utilityScript.CameraTopLeft;
        cameraTopRight = utilityScript.CameraTopRight;

        initPlatform.GetComponent<PlatformMover>().moveSpeed = platformSpeed;
        platforms.Add(initPlatform);
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
            if(utilityScript.IsObjectBelowCamera(platforms[i].transform))
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
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
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, UnityEngine.Quaternion.identity);
        newPlatform.GetComponent<PlatformMover>().moveSpeed = platformSpeed;
        platforms.Add(newPlatform);
        lastX = spawnPosition.x;
    }


    public void scrollDown(float distance)
    {
        for(int i = platforms.Count - 1; i >= 0; i--)
        {
            PlatformMover mover = platforms[i].GetComponent<PlatformMover>();
            if (mover != null)
            {
                mover.StartMoving();
            }
        }
    }
}
