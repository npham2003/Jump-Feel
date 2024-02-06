using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimateShade : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text text;
    private int shade = 220;

    public bool up = false;
    void Start()
    {
        text = this.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(up){
            shade += 1;
        }else{
            shade-=1;
        }

        if(shade<=170){
            up = true;
        }
        if(shade>=240){
            up = false;
        }
        text.color = new Color(shade/255f, shade/255f, shade/255f, 1f);
    }

    
}
