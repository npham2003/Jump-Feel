using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningScript : MonoBehaviour
{

    public PlatformGenerator platformGenerator;

    public GameObject background1;
    public GameObject background2;
    public GameObject missileSpawner;
    public Canvas canvas;

    public float moveSpeed;
    public int playerHealth = 3;
    private float backgroundHeight;
    public bool gameOver = true;




    // Start is called before the first frame update
    void Start()
    {
        platformGenerator.platformSpeed = moveSpeed;
        missileSpawner.GetComponent<MissleSpawnScript>().missiles = true;
        backgroundHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
        ResetBackground();
        //UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {


        //backgroudn scroll
        background1.transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        background2.transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (background1.transform.position.y <= -backgroundHeight)
        {
            background1.transform.position += Vector3.up * 2 * backgroundHeight;
        }
        if (background2.transform.position.y <= -backgroundHeight)
        {
            background2.transform.position += Vector3.up * 2 * backgroundHeight;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }



    void ResetBackground()
    {
        background1.transform.position = new Vector3(0, 0, 5);
        background2.transform.position = new Vector3(0, backgroundHeight, 5);
    }



}
