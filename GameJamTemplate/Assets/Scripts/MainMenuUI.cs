using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuUI : MonoBehaviour
{
    public Sprite s;
    public void StartGame(){
        GameManager.LoadLevel(1);
    }
    public void Quit(){
        GameManager.QuitGame();
    }
}
