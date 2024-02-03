using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnScript : MonoBehaviour
{
    public GameObject missle;
    private float cameraHeight;
    private float cameraWidth;
    private float timer = 0;
    public float spawnRate = 2; // use random spawn rate later

    public float moveSpeed;
    public float rate = 4f;

    // Start is called before the first frame update
    void Start()
    {
        CalculateCameraHeightandWidth();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            SpawnMissle();
            timer = 0;
        }
        if(this.transform.position.y < -cameraHeight)
        {
            Destroy(missle);
        }
    }

    void SpawnMissle()
    {
        float spawnX = Random.Range(-cameraWidth / 2, cameraWidth / 2);
        float spawnY = cameraHeight/2;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        GameObject newMissle = Instantiate(missle, spawnPosition, UnityEngine.Quaternion.identity);
        Rigidbody2D missleRb = newMissle.GetComponent<Rigidbody2D>();
        missleRb.simulated = false;
        StartCoroutine(waitToShoot(missleRb));
    }

    IEnumerator waitToShoot(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(1.5f);
        rb.simulated = true;
    }

    void CalculateCameraHeightandWidth()
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
        }
        else
        {
            Debug.LogError("Camera is not orthographic.");
        }
    }
}