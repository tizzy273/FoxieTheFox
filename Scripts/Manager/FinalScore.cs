using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalScore : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI textScore = null;
   [SerializeField] TextMeshProUGUI textGems = null;
   [SerializeField] TextMeshProUGUI textCherries = null;
   [SerializeField] TextMeshProUGUI textTimer = null;

    public void setup()
    {
        float score = GameManager.instance.score;
        float levelMaxTime = 400000;  //6,6 minuti
        float timeSpent = (float) TimeManager.instance.timePlaying.TotalMilliseconds;
        float finalScore = Mathf.Ceil(((levelMaxTime - timeSpent)* 1 + score*5) / 6); // Media ponderata
        string totalTime = PlayerPrefs.GetString("TimeCount");

        
        Debug.Log(TimeManager.instance.timePlaying.TotalMilliseconds);
        float totalScore = GameManager.instance.score /(float) TimeManager.instance.timePlaying.TotalMilliseconds;
       
        textCherries.text = GameManager.instance.cherries.ToString();
        textScore.text = "Total Score: " + finalScore.ToString();
        Save.instance.SaveFinaleScore(finalScore);
  
        textGems.text =  GameManager.instance.gems.ToString();
        textTimer.text = totalTime;  
    } 

    private void Start()
    {
        setup();
    }

}
