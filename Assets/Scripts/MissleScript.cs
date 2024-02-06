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

    private UtilityScript utilityScript;

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
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
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
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
        if(utilityScript.IsObjectBelowCamera(transform))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(player!=null){
            CharacterScript characterScript = player.GetComponent<CharacterScript>();
        
            if (other.gameObject.CompareTag("MissleTrigger") && !characterScript.hitByMissle)
            {
                characterScript.hitByMissle = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().playerHit();
                shakeBehavior.triggerShake();
                Destroy(gameObject);
                
            }
        }
    }
}
