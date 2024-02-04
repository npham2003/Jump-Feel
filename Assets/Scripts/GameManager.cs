using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlatformGenerator platformGenerator;
    public CharacterScript player;
    public GameObject background1;
    public GameObject background2;


    public Toggle[] toggles;

    public float moveSpeed;
    public float gravity;
    public float jumpForce;

    private float backgroundHeight;

    // Start is called before the first frame update
    void Start()
    {
        platformGenerator.platformSpeed = moveSpeed;
        player.defaultDownSpeed = moveSpeed;
        player.jumpForce = jumpForce;
        player.gravity = gravity;
        InitializeToggles();

        backgroundHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
        ResetBackground();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y >= 1.5f)
        {   
            //player.transform.Translate(0,-2f,0);
            platformGenerator.scrollDown(2f);

        }

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

    }

    void InitializeToggles()
    {
        foreach(Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate {OnToggleValueChanged(toggle);});
        }
    }

    public void OnToggleValueChanged(Toggle changedToggle)
    {

        if(changedToggle.name == "TrailToggle")
        {
            player.GetComponent<MyTrailRenderer>().trailing = changedToggle.isOn;
        }
    }

    void ResetBackground()
    {
        background1.transform.position = new Vector3(0,0,5);
        background2.transform.position = new Vector3(0,backgroundHeight,5);
    }

}
