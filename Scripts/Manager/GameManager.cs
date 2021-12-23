using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    [System.NonSerialized] public bool credits = false;

   
   
    [System.NonSerialized] public string hCommands;
    [System.NonSerialized] public string fireCommand;

    private GameObject player;

    public static GameManager instance = null;
    [System.NonSerialized]  public float gems = 0;
    [System.NonSerialized] public float cherries = 0;
    [System.NonSerialized] public float score = 0;
    [System.NonSerialized] public float health = 3;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


        hCommands = "arrows";
        fireCommand = "Firez";
    }
  
    private void Update()
    {
       
    
    }

    

    private void Start()
    {
        AudioManager.instance.PlayTrack(AudioManager.Track.MainMenu);
    }


    public void StartGame()
    {
        StartCoroutine(WaitStartGame());
    }
    public IEnumerator WaitStartGame()
    {
        Reset();

        SceneManager.LoadScene("Level 1");
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();  //Wait for loading new scene
        AudioManager.instance.PlayTrack(AudioManager.Track.Play);
        TimeManager.instance.StartTimer();
    }

    public void ContinueGame()
    {
        StartCoroutine(WaitContinueGame());
    }

    public IEnumerator WaitContinueGame()
    {
        Reset();
        SceneManager.LoadScene("Level 1");
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame(); 
        AudioManager.instance.PlayTrack(AudioManager.Track.Play);
        float[] progress = Save.instance.getProgress();
        instance.health = progress[(int) Save.ProgressName.health];
        instance.score = progress[(int) Save.ProgressName.score];
        instance.cherries = progress[(int) Save.ProgressName.cherries];
        instance.gems = progress[(int) Save.ProgressName.gems];
        TimeManager.instance.elapsedTime = progress[(int) Save.ProgressName.time];
        TimeManager.instance.resumeTimer();
        RestartFromCheckpoint();
    }

    public void GameOver() {


        PlayerPrefs.Save();
        AudioManager.instance.trackAudioSource.Stop();
        TimeManager.instance.StopTimer();
        SceneManager.LoadScene("GameOver");
        
        
        AudioManager.instance.PlayTrack(AudioManager.Track.GameOver);
       
          
    
       }

 

   

    public void RestartFromCheckpoint()
    {
      
        player = GameObject.Find("Player");
        player.transform.position = Save.instance.GetPosition();
        Debug.Log("CUORERIMOSSO()");
        Physics2D.IgnoreLayerCollision(9, 10, false);
        

    }     

    public void Restart()
    {
        StartCoroutine(WaitRestart());
    }

    IEnumerator WaitRestart()
    {

        Reset();
        
        SceneManager.LoadScene("Level 1");
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        AudioManager.instance.PlayTrack(AudioManager.Track.Play);
        TimeManager.instance.StartTimer();
    }
    private void Reset()
    {
        Physics2D.IgnoreLayerCollision(9, 10, false);
        instance.cherries = 0;
        instance.gems = 0;
        instance.score = 0;
        instance.health = 3;
        TimeManager.instance.elapsedTime = 0;
    }
   

}
