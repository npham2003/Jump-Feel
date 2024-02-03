using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlatformGenerator platformGenerator;
    public CharacterScript player;
    public ShakeBehavior shakeBehavior;
    public Toggle[] toggles;

    public float moveSpeed;
    public float gravity;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        platformGenerator.platformSpeed = moveSpeed;
        player.defaultDownSpeed = moveSpeed;
        player.jumpForce = jumpForce;
        player.gravity = gravity;
        InitializeToggles();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y >= 1.5f)
        {   
            //player.transform.Translate(0,-2f,0);
            platformGenerator.scrollDown(2f);

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

        if (changedToggle.name == "TrailToggle")
        {
            player.GetComponent<MyTrailRenderer>().trailing = changedToggle.isOn;
        }
        if (changedToggle.name == "ScreenShakeToggle")
        {
            shakeBehavior.shakeOn = changedToggle.isOn;
        }
        if (changedToggle.name == "BlinkToggle")
        {
            player.blinkOn = changedToggle.isOn;
        }
    }

}
