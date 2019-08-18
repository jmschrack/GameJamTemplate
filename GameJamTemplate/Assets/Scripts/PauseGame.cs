using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseGame : GlobalEventBehavior
{
   public GameObject PauseScreenCanvas;
   bool isPaused;
    
    public override void Init(){
        //Just to make sure we don't leave the canvas active on accident
        PauseScreenCanvas.SetActive(false);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused)
                GameManager.UnpauseGame();
            else
                GameManager.PauseGame();
        }
    }
    /*
    We have this here so we can easily link our UI buttons into the GameManager, 
    which doesn't exist in this scene at Design Time.
    It also allows us to do custom scripting if need be before issuing the global Unpause command
    */
    public void Unpause(){
        GameManager.UnpauseGame();
    }
    public void QuitGame(){
        //You could play like an audio cue or prompt to save the game or whatever
        GameManager.QuitGame();
    }
    public override void OnPause(){
        isPaused=true;
        PauseScreenCanvas.SetActive(true);
        

        /*
        Setting time scale to 0 stops the game engine's clock.  So anything that is dependent on time will stop.
        Like coroutines, navAgents, AI, etc.
        HOWEVER! This just freezes the clock and nothing else. So if you wrote some code
        that doesn't go off of the clock, that will continue on. 
        You can also set this to 0.5f for "slowmo" or 1.5f for "fastforward."
        Or any other decimal value you want
         */
        Time.timeScale=0;
    }
    public override void OnUpaused(){
        isPaused=false;
        PauseScreenCanvas.SetActive(false);
        Time.timeScale=1;
    }
}
