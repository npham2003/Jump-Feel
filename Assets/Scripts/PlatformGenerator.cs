using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public int numberOfPlatforms = 20;
    public float levelWidth = 3f;
    public float minY = .2f;
    public float maxY = 1.5f;
    private Vector3 spawnPosition = new Vector3();

    // Start is called before the first frame update
    void Start()
    {  
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(PlatformPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
