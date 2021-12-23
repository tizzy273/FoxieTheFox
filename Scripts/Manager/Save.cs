using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Save : MonoBehaviour
{
    // Start is called before the first frame update
    public static Save instance;
    string username;
    MainMenu mainMenu;

   public enum ProgressName
    {
        health,
        score,
        cherries,
        gems,
        time,
    }


    
    //ProgressName progressName;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        mainMenu = GameObject.Find("Background").GetComponent<MainMenu>();
    }


    public void SaveUsername(string username)
    {
        SetUsername(username);
        AddUsername(username);
        PlayerPrefs.SetString(username, username);
    }

    void AddUsername(string username) // Usato per la classifica
    {
        if (!PlayerPrefs.HasKey("Players"))
            PlayerPrefs.SetString("Players", username);
        else
        {
            string usernames = PlayerPrefs.GetString("Players");
            PlayerPrefs.SetString("Players", usernames + " " + username);
        }


    }


  

  

    public void SetUsername(string username)
    {
        this.username = username;
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetFloat(username + "Score", GameManager.instance.score);
        PlayerPrefs.SetFloat(username + "Health", GameManager.instance.health);
        PlayerPrefs.SetFloat(username + "Cherries", GameManager.instance.cherries);
        PlayerPrefs.SetFloat(username + "Gems", GameManager.instance.gems);
        PlayerPrefs.SetFloat(username + "Time", TimeManager.instance.elapsedTime);
    }      


    
   public void SaveFinaleScore(float finalScore) //registra lo score solo se è maggiore del precedente.
    {
        if (PlayerPrefs.HasKey(username + "FinalScore"))
        {
            if (PlayerPrefs.GetFloat(username + "FinalScore") < finalScore)
                PlayerPrefs.SetFloat(username + "FinalScore", finalScore);
        }
        else
            PlayerPrefs.SetFloat(username + "FinalScore", finalScore);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetFloat(username + "Health", 3);
        PlayerPrefs.SetFloat(username + "Score", 0);
        PlayerPrefs.SetFloat(username + "Cherries", 0);
        PlayerPrefs.SetFloat(username + "Gems", 0);
        PlayerPrefs.SetFloat(username + "Time", 0);
        SavePosition(new Vector3(-31.54f, 1.5f, 0)); //firstSpawnPoint
        
    }
    


   public void SavePosition(Vector3 position) //to use in checkpoint
    {
        PlayerPrefs.SetFloat(username + "XPosition", position.x);
        PlayerPrefs.SetFloat(username + "YPosition", position.y);
        PlayerPrefs.SetFloat(username + "ZPosition", position.z);
    }



    public  Vector3 GetPosition()
    {
        float Xposition = PlayerPrefs.GetFloat(username + "XPosition");
        float Yposition = PlayerPrefs.GetFloat(username + "YPosition") + 0.5f;
        float Zposition = PlayerPrefs.GetFloat(username + "ZPosition");

        return new Vector3(Xposition, Yposition, Zposition);
    }


    string getUsername()
    {
        if (PlayerPrefs.HasKey(username))
            return PlayerPrefs.GetString(username);
        return null;
    }

   public float[] getProgress()
    {

        float[] progress = new float[5];
        if (PlayerPrefs.HasKey(username))
        {
            progress[(int)ProgressName.health] = PlayerPrefs.GetFloat(username + "Health", GameManager.instance.health);
            progress[(int)ProgressName.score] = PlayerPrefs.GetFloat(username + "Score", GameManager.instance.score);
            progress[(int)ProgressName.cherries] = PlayerPrefs.GetFloat(username + "Cherries", GameManager.instance.cherries);
            progress[(int)ProgressName.gems] = PlayerPrefs.GetFloat(username + "Gems", GameManager.instance.gems);
            progress[(int)ProgressName.time] = PlayerPrefs.GetFloat(username + "Time", TimeManager.instance.elapsedTime);

           // progress[(int)ProgressName.XPosition] = PlayerPrefs.GetFloat(username + "", TimeManager.instance.elapsedTime);

            return progress;

        }
        return null;  
    }



    Vector3 getPosition()
    {
        if (PlayerPrefs.HasKey(username))
        {
           float XPosition = PlayerPrefs.GetFloat(username + "XPosition");
           float YPosition = PlayerPrefs.GetFloat(username + "YPosition");
           float ZPosition = PlayerPrefs.GetFloat(username + "ZPosition");

          return new Vector3(XPosition, YPosition, ZPosition);
        }
        Debug.Log("User Inesistente!");
        return new Vector3(0, 0, 0);
    }

}
