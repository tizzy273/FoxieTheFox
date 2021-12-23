using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    TextMeshProUGUI timeCounter;
   [System.NonSerialized] public float elapsedTime = 0f;

    public TimeSpan timePlaying;
    private bool timerGoing;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        
      //  DontDestroyOnLoad(timeCounter);
    }

    public void Start()
    {
       
    }


    public void StartTimer()
    {

        // OnSceneLoaded(SceneManager.GetActiveScene() , LoadSceneMode.Single);
        timeCounter = GameObject.Find("TimerTextUI").GetComponent<TextMeshProUGUI>();
        timerGoing = true;
        elapsedTime = 0;
        Debug.Log(elapsedTime);
        StartCoroutine(UpdateTimer());
       // StartCoroutine(Wait());
           
        

        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        
        

    }



    public void StopTimer()
    {
        PlayerPrefs.SetString("TimeCount", timePlaying.ToString("mm':'ss':'ff"));
        timerGoing = false;
    }

    public void resumeTimer()
    {
        timeCounter = GameObject.Find("TimerTextUI").GetComponent<TextMeshProUGUI>();
        timerGoing = true;
        StartCoroutine(UpdateTimer());
    }
    IEnumerator UpdateTimer()
    {

        
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            //Debug.Log("Elapsed "+elapsedTime);
           // Debug.Log(timeCounter);
           timeCounter.text = timePlaying.ToString("mm':'ss':'ff");
           // Debug.Log("tempo" + timePlaying.ToString("mm':'ss':'ff"));
            yield return null;
       }
 
    }
}
