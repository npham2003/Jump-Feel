using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreKeeper : MonoBehaviour
{
    public static HighScoreKeeper keeper;
    public int highScore=0;

    private void Awake()
    {
        keeper = this;
        DontDestroyOnLoad(gameObject);
    }
}
