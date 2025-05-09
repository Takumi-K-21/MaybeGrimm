using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    public GameObject Select_01;
    public GameObject Select_02;

    public void OnPrologue()
    {
        // SceneManager.LoadScene("Prologue");
    }

    public void OnTutorial()
    {
        SceneManager.LoadScene("BattleScene(Hunter)");
    }

    public void OnFirstChapter()
    {
        // SceneManager.LoadScene("BattleScene(Hunter)");
    }

    public void OnFirstBattle()
    {
        SceneManager.LoadScene("BattleScene(Queen)");
    }

    public void OnSecondChapter()
    {
        // SceneManager.LoadScene("BattleScene(Hunter)");
    }

    public void OnSecondBattle()
    {
        SceneManager.LoadScene("BattleScene(Gretel)");
    }

    public void OnThirdChapter()
    {
        // SceneManager.LoadScene("BattleScene(Gretel)");
    }

    public void OnThirdBattle()
    {
        // SceneManager.LoadScene("BattleScene(Gretel)");
    }

    public void OnLastChapter()
    {
        // SceneManager.LoadScene("BattleScene(Gretel)");
    }

    public void OnLastBattle()
    {
        // SceneManager.LoadScene("BattleScene(Gretel)");
    }

    public void OnEnding()
    {
        // SceneManager.LoadScene("BattleScene(Gretel)");
    }

    public void OnNext()
    {
        Select_01.SetActive(false);
        Select_02.SetActive(true);
    }

    public void OnBack()
    {
        Select_01.SetActive(true);
        Select_02.SetActive(false);
    }

    public void OnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
