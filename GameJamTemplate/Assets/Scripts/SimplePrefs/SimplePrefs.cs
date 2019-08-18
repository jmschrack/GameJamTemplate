using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SimplePrefs))]
public class SimplePrefsEditor : Editor{
    public override void OnInspectorGUI(){
        SimplePrefs sp = target as SimplePrefs;
        sp.LoadOnStart=EditorGUILayout.Toggle("Load File On Start",sp.LoadOnStart);
        List<string> keys=sp.GetAllKeys();
        if(keys==null){
            if(GUILayout.Button("Test Load")){
                sp.LoadFile();
            }
        }else{
            foreach(string s in keys){
                EditorGUILayout.LabelField(s+"="+sp.Get(s,""));
            }
        }
    }
}
#endif

/**
A simple preferences loader and saver. 
Does what it says on the tin.
*/

public class SimplePrefs : MonoBehaviour
{
    private static readonly string _FILENAME="settings.conf";
    private Dictionary<string,string> props;
    public bool LoadOnStart=true;
    void Start(){
        Init();
        if(LoadOnStart){
            LoadFile();
        }
    }
    void Init(){
        props= new Dictionary<string, string>();
        /*
        -------------------------
        Put default settings here
        copy/paste the below as many times as needed
        (don't forget to remove or replace the joke settings below)
        -------------------------
         */
        props.Add("SomeKey1","SomeValue");
        props.Add("IDunno","SomethingElse");
        props.Add("Whatever","Whatcha");
    }
    
    
    ///<param name="key">The value you want</param>
    ///<param name="def">Optional value to return, if it couldn't find the requested one</param>
    /// <returns>Returns the value or the specified default, which is null if unused.</returns>
    public string Get(string key, string def=null){
        if(props.ContainsKey(key))
            return props[key];
        return def;
    }
    public List<string> GetAllKeys(){
        if(props==null)
            return null;
        List<string> temp = new List<string>();
        temp.AddRange(props.Keys);
        return temp;
    }

    public void Set(string key, string value)
    {
        props[key]=value;
    }

    public void LoadFile(){
        if(!File.Exists(_FILENAME)){
            //We didn't find anything, so let's just leave....
            Debug.Log("No settings file found. Just gonna use defaults");
            return;
        }
        if(props==null)
            Init();
        string[] p=File.ReadAllLines(_FILENAME);
        //Debug.LogFormat("Found settings file, loaded {0} lines.",p.Length);

        for(int i=0;i<p.Length;i++){
            string[] line = p[i].Split('=');
            if(line.Length>1){
                //check if we already have a variable for that
                if(props.ContainsKey(line[0])){
                    //override
                    props[line[0]]=line[1];
                    //Debug.LogFormat("Setting {0} to {1}",line[0],line[1]);
                }else{
                    //it's new!
                    props.Add(line[0],line[1]);
                    //Debug.LogFormat("New Setting {0} to {1}",line[0],line[1]);
                }
                
            }else{
                Debug.LogWarningFormat("Found Invalid Line In settings! {0}",p[i]);
            }
        }
    }
    public void SaveFile(){
        StringBuilder sb = new StringBuilder();
        foreach(string key in props.Keys){
            sb.AppendFormat("{0}=",key);
            sb.AppendLine(props[key]);
        }
        File.WriteAllText("settings.conf",sb.ToString());
    }
    
}
