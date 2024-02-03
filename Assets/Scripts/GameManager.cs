using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlatformGenerator platformGenerator;
    public CharacterScript player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y >= 1.5f)
        {   
            player.transform.Translate(0,-2f,0);
            platformGenerator.scrollDown(2f);

        }
    }
}
