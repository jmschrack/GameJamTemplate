using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //If you add events to this, add them to the end. 
    public enum GlobalEventType{
        LevelStart,
        LevelEnd,
        Paused,
        Unpaused,
    }
    public delegate void GlobalEvent(GlobalEventType t);
    public static bool AddListener(GlobalEvent globalEvent){
        if(Instance!=null){
            Instance.GameManagerEvent+=globalEvent;
            return true;
        }
        return false;
    }
    public static void RemoveListener(GlobalEvent globalEvent){
        if(_instance!=null){
            _instance.GameManagerEvent-=globalEvent;
        }
    }
    public static void BroadcastEvent(GlobalEventType eventType){
        if(_instance.GameManagerEvent!=null){
            _instance.GameManagerEvent(eventType);
        }
    }
    /*
    We use a "Toolbox" design pattern here.
    The GameManager is the only singleton allowed in the project
     */
    private static GameManager _instance;
    private static bool _shuttingDown;
    public static GameManager Instance{
        get{
            if(_shuttingDown)
                return null;
            if(_instance!=null)
                return _instance;
            GameObject temp = new GameObject();
            temp.name="GameManager (Created Dynamically)";
            GameManager gm = temp.AddComponent<GameManager>();
            gm.SingletonCheck();
            return gm;
        }
    }
    public static SimplePrefs UserPreferences{
        get{return Instance._simplePrefs;}
    }

    //strings are the devil, but it's a Gamejam so who cares
    Dictionary<string,string> _globalGameVars= new Dictionary<string, string>();
    public static string GetGlobalVar(string key,string def=null){
        if(Instance._globalGameVars.ContainsKey(key)){
            return Instance._globalGameVars[key];
        }
        return def;
    }
    public static void SetGlobalVar(string key,string value){
         if(Instance._globalGameVars.ContainsKey(key)){
            Instance._globalGameVars[key]=value;
        }else{
            Instance._globalGameVars.Add(key,value);
        }
    }
   
    /// <param name="level">Unless you changed the scene list order,MainMenu is "0" and Level 1 is "1", etc</param>
    public static void LoadLevel(int level){
         UnityEngine.SceneManagement.SceneManager.LoadScene(level);
        
    }
    public static void PauseGame(){
        BroadcastEvent(GameManager.GlobalEventType.Paused);
    }
    public static void UnpauseGame(){
        BroadcastEvent(GameManager.GlobalEventType.Unpaused);
    }
    public static void QuitGame(){
        Application.Quit();
    }
    /*
    ---------------------------
    Local Variables and Methods
    ---------------------------
     */
    //Prevents constructor use
    protected GameManager(){ }
    [SerializeField]
    private SimplePrefs _simplePrefs;
    GlobalEvent GameManagerEvent;
    private bool initd=false;
    void Awake(){
        SingletonCheck();
    }
    void SingletonCheck(){
        if(initd)
            return;
        if(_instance==null){
            _instance=this;
            DontDestroyOnLoad(this);
        }else if(_instance!=this){
            Destroy(this);
            Destroy(this.gameObject);
        }
        initd=true;
    }
    void Start(){
        if(_simplePrefs==null){
            _simplePrefs=GetComponent<SimplePrefs>();
        }
        
    }
    
    void OnDestroy(){
        if(_instance==this){
            _instance=null;
            _shuttingDown=true;
        }
    }
    
}
