using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject creditsObject = null;

    public void TryAgain()
    {
        Save.instance.ResetProgress();
        GameManager.instance.Restart();
    }

    private void Start()
    {
        ShowCredits();
    }

    public void ShowCredits()
    {
        if (GameManager.instance.credits == true)
        {
            creditsObject.SetActive(true);
            StartCoroutine(EndCredits());
        }
    }

    IEnumerator EndCredits()
    {
        yield return new WaitForSeconds(12);
        creditsObject.SetActive(false);
        GameManager.instance.credits = false;
    }

    public void MainMenu()
    {
        Save.instance.ResetProgress();
        SceneManager.LoadScene("MainMenu");
    }
}
