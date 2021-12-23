using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] public TextMeshProUGUI newUsernametxt;
    [SerializeField] public TextMeshProUGUI loadUsernametxt;
    public GameObject seiSicuro;
    public GameObject newGameMenu;
    public GameObject usernameNonEsistente;
    public GameObject usernameGiaEsistente;
    public GameObject loadGameMenu;
    string username = null;
    public GameObject tutorial;


    [SerializeField] private TMP_Dropdown dropdownMenu = default;
    [SerializeField] private Toggle isFullscreen = default;
    private Resolution[] myResolutions;



    [SerializeField] TMP_Dropdown controls = default;

    [SerializeField]
    public TextMeshProUGUI[] ranking;




    public void PlayGame()
    {
        string username = newUsernametxt.text.ToString().Substring(0, newUsernametxt.text.ToString().Length - 1);
        if (!username.Contains(" "))
        {


            if (PlayerPrefs.HasKey(username))
            {
                Save.instance.SetUsername(username);
                Debug.Log("Username già utilizzato!" + username);
                seiSicuro.SetActive(true);
                newGameMenu.SetActive(false);
                return;
            }
            else
            {
                Save.instance.SaveUsername(username);
                tutorial.SetActive(true);
            }
        }

    }

    public void PlayNewGame()
    {
        GameManager.instance.StartGame();
    }

    public void UsernameAlreadyInUse()
    {
        usernameGiaEsistente.SetActive(true);
        newGameMenu.SetActive(false);
    }

    public void UsernameDoesntExist()
    {
        usernameNonEsistente.SetActive(true);
        loadGameMenu.SetActive(false);
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void Continue()
    {
        string username = loadUsernametxt.text.ToString().Substring(0, loadUsernametxt.text.ToString().Length - 1);
        if (PlayerPrefs.HasKey(username))
        {
            Save.instance.SetUsername(username);
            GameManager.instance.ContinueGame();
        }
        else
        {
            Debug.Log("Username inesistente" + username);
            UsernameDoesntExist();
        }
    }

    public void ToggleSoundTrack()
    {
        AudioManager.instance.trackAudioSource.mute = !AudioManager.instance.trackAudioSource.mute;
    }
    
    public void ToggleSoundEffects()
    {
        AudioManager.instance.soundAudioSource.mute = !AudioManager.instance.soundAudioSource.mute;
    }

    public string getUsername()
    {
        return username;
    }



    public void Resolutions() 
    {
        myResolutions = Screen.resolutions;
        dropdownMenu.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < myResolutions.Length; i++)
        {
            
            string option = myResolutions[i].ToString(); 
            options.Add(option);
            if (myResolutions[i].width == Screen.width && myResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        dropdownMenu.AddOptions(options);
        dropdownMenu.value = currentResolutionIndex;
        dropdownMenu.RefreshShownValue();

    }

   
        public void ChangeResolution(int index)
        {
            Screen.SetResolution(myResolutions[index].width, myResolutions[index].height, isFullscreen.isOn);
        }
    
    public void setFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
   public void Controls()
    {


        if (controls.value == 1)
        {
            GameManager.instance.hCommands = "arrows";
            GameManager.instance.fireCommand = "Firez";
            Debug.Log("arrows");
           
        }
        else if (controls.value == 2)
        {
            GameManager.instance.hCommands = "ad";
            GameManager.instance.fireCommand = "Firel";
            Debug.Log("ad");
        }
        
    }



   


    public void MakeRank()
    {
        string[] players = PlayerPrefs.GetString("Players").Split(' ');
        Debug.Log("players");

        List<Tuple<float, string>> rank = new List<Tuple<float, string>>();


        for (int i = 0; i < players.Length; i++)
        {


            if (PlayerPrefs.HasKey(players[i] + "FinalScore"))
            {

                float finalScore = PlayerPrefs.GetFloat(players[i] + "FinalScore");
                rank.Add(new Tuple<float, string>(finalScore, players[i]));
            }
        }

        rank.Sort(Comparer<Tuple<float, string>>.Default);
        rank.Reverse();

        int position = 1;


            int j = 0;
            foreach (var tuple in rank)
            {
                ranking[j].text = position + "st " + tuple.Item2 + "     " + tuple.Item1;
                position++;
                j++;
                if (j == 7)
                    break;
            }
            
     
    }
    



}

