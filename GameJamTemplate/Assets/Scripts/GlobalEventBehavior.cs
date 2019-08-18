using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventBehavior : MonoBehaviour
{
    void Start()
    {
        GameManager.AddListener(GlobalEventHandler);
        Init();
    }
    void OnDestroy(){
        GameManager.RemoveListener(GlobalEventHandler);
    }
    
    public void GlobalEventHandler(GameManager.GlobalEventType eventType){
        switch(eventType){
            case GameManager.GlobalEventType.Paused:
            OnPause();
            break;
            case GameManager.GlobalEventType.Unpaused:
            OnUpaused();
            break;
            case GameManager.GlobalEventType.LevelEnd:
            OnLevelEnd();
            break;
            case GameManager.GlobalEventType.LevelStart:
            OnLevelStart();
            break;
        }
    }

    public virtual void Init(){

    }
    public virtual void OnPause(){

    }
    public virtual void OnUpaused(){
        
    }
    public virtual void OnLevelStart(){

    }
    public virtual void OnLevelEnd(){

    }
}
