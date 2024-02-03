using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float deadZone = -15;
    public GameObject player;
    public GameObject mainCamera;
    public ShakeBehavior shakeBehavior;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
            if(player == null)
            {
                Debug.Log("Player not found");
            }
        }
        else
        {
            Debug.Log("find player");
        }
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
            if(mainCamera == null)
            {
                Debug.Log("Camera not found");
            }
            shakeBehavior = mainCamera.GetComponent<ShakeBehavior>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterScript characterScript = player.GetComponent<CharacterScript>();
        if (other.gameObject.CompareTag("MissleTrigger") && !characterScript.hitByMissle)
        {
            characterScript.hitByMissle = true;
            shakeBehavior.triggerShake();
            Destroy(gameObject);
            
        }
    }
}
