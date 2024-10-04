using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeLevelTxt;
    private float timeLevel;
    private bool pause = false;
    public void botaoPause() {
        pause = !pause;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pause == false) {
            timeLevel = timeLevel + Time.deltaTime;
            timeLevelTxt.text = timeLevel.ToString("F0");
        }
    }
}
