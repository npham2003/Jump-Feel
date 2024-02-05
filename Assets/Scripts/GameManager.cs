using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlatformGenerator platformGenerator;
    public CharacterScript player;

    public ShakeBehavior shakeBehavior;

    public GameObject background1;
    public GameObject background2;
    public GameObject missileSpawner;
    public GameObject heartPrefab;
    public Canvas canvas;


    public Toggle[] toggles;

    public float maxSpeed;
    public float moveSpeed;
    public float gravity;
    public float jumpForce;
    public TMP_Text scoreText;
    public int score=0;
    public int playerHealth = 3;

    public float scoreMultiplier = 1;
    private float backgroundHeight;
    public bool gameOver = false;

    public float gameSpeedIncrese = 0.001f;
    public bool speedIncreaseOn = false;

    public TMP_Text gameOverScoreText;

    public TMP_Text highScoreText;

    public GameObject gameOverPanel;


    // Start is called before the first frame update
    void Start()
    {
        platformGenerator.platformSpeed = moveSpeed;
//        player.defaultDownSpeed = moveSpeed;
        player.jumpForce = jumpForce;
        player.gravity = gravity;
        InitializeToggles();

        backgroundHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
        ResetBackground();
        StartCoroutine(Score());
        //UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y >= 1.5f)
        {   
            //player.transform.Translate(0,-2f,0);
            platformGenerator.scrollDown(2f);

        }
        if (moveSpeed < maxSpeed && speedIncreaseOn)
        {
            moveSpeed += gameSpeedIncrese;
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

        if(gameOver){
            if(Input.GetKeyDown(KeyCode.Space)){
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }

    }

    void InitializeToggles()
    {
        foreach(Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate {OnToggleValueChanged(toggle);});
        }
        
    }

    public void GameOver(){
        gameOver=true;
        gameOverScoreText.text=score.ToString();
        gameOverPanel.SetActive(true);
        if(score>HighScoreKeeper.keeper.highScore){
            HighScoreKeeper.keeper.highScore = score;
        }
        highScoreText.text = HighScoreKeeper.keeper.highScore.ToString();
    }

    public void OnToggleValueChanged(Toggle changedToggle)
    {
        if(changedToggle.name == "SelectAllToggle")
        {
            setAllEffects(changedToggle.isOn);
        }

        if(changedToggle.name == "DeSelectAllToggle")
        {
            setAllEffects(false);
        }

        if (changedToggle.name == "TrailToggle")
        {
            Debug.Log("Toggle Trail");
            player.GetComponent<MyTrailRenderer>().trailing = changedToggle.isOn;
            player.GetComponent<CharacterScript>().ChangeSpeed(changedToggle.isOn);
            if(changedToggle.isOn){
                scoreMultiplier+=0.2f;
            }else{
                scoreMultiplier-=0.2f;
            }
        }
        if (changedToggle.name == "ScreenShakeToggle")
        {
            shakeBehavior.shakeOn = changedToggle.isOn;
            missileSpawner.GetComponent<MissleSpawnScript>().missiles = changedToggle.isOn;         
            if(changedToggle.isOn){
                UpdateHealthUI();
                scoreMultiplier+=0.4f;
            }else{
                DeleteHealthUI();
                scoreMultiplier-=0.4f;
            }
        }
        if (changedToggle.name == "BlinkToggle")
        {
            player.blinkOn = changedToggle.isOn;
        }
        if(changedToggle.name == "DustToggle")
        {
            speedIncreaseOn = changedToggle.isOn;
            player.dustOn = changedToggle.isOn;
            if(changedToggle.isOn){
 //                player.GetComponent<CharacterScript>().jumpForce=800;
 //                player.GetComponent<Rigidbody2D>().gravityScale=4;
                scoreMultiplier+=0.3f;
            }
            else{
 //               player.GetComponent<CharacterScript>().jumpForce=400;
 //               player.GetComponent<Rigidbody2D>().gravityScale=1;
                scoreMultiplier-=0.3f;
            }   
        }
        if (changedToggle.name == "Sounds") {
            player.soundsOn = changedToggle.isOn;
            missileSpawner.GetComponent<MissleSpawnScript>().soundOn = changedToggle.isOn;
        }
    }

    private void setAllEffects(bool isEffectOn)
    {
        foreach (Toggle toggle in toggles)
        {
            if (toggle.name == "DeSelectAllToggle" && isEffectOn) continue;
            toggle.isOn = isEffectOn;

        }
    }
         

    void ResetBackground()
    {
        background1.transform.position = new Vector3(0,0,5);
        background2.transform.position = new Vector3(0,backgroundHeight,5);
    }

    private IEnumerator Score(){
        for(; ;){
            
            yield return new WaitForSeconds(1);
            if(!gameOver){
                score+=((int)(100*scoreMultiplier));
                scoreText.text="Score: "+score;
            }
        }
    }

    public void UpdateHealthUI()
    {
        DeleteHealthUI();
        for (int i = 0; i < playerHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, canvas.transform);
            heart.tag = "HeartUI";

            RectTransform rect = heart.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(330 - (rect.sizeDelta.x /2 * i), 200);
        }
    }

    void DeleteHealthUI()
    {
        foreach (Transform child in canvas.transform)
        {
            if (child.gameObject.CompareTag("HeartUI"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void playerHit()
    {
        playerHealth--;
        UpdateHealthUI();
        if(playerHealth <= 0)
        {
            Destroy(player);
            GameOver();
        }
    }

}
