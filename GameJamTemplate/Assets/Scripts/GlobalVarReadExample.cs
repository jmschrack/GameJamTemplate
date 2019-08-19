using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVarReadExample : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject LosePanel;
    // Start is called before the first frame update
    void Start()
    {
        string gameEnd=GameManager.GetGlobalVar("GameEnd");
        if(gameEnd!=null&&gameEnd.Equals("Win")){
            WinPanel.SetActive(true);
        }else{
            //if it doesn't say win, we don't care so say they lost somehow
            LosePanel.SetActive(true);
        }
    }

    public void GoToMainMenu(){
        GameManager.SetGlobalVar("GameEnd",null);
        GameManager.LoadLevel(0);
    }
}
