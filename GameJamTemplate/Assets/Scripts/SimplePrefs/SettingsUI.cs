using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public SettingsFieldUI SettingsFieldPrefab;
    public Transform contentRoot;
    public bool ShowAllSettings=true;
    
    [Tooltip("This is overridden by ShowAllSettings")]
    public List<string> specificSettings= new List<string>();
    List<SettingsFieldUI> fields;
    
    public void ShowUI(){
        if(fields==null){
            fields=new List<SettingsFieldUI>();
            List<string> keys;
            if(ShowAllSettings){
                keys= GameManager.UserPreferences.GetAllKeys();
            }else{
                keys=specificSettings;
            }
            //spawn a bunch of entries, one for each setting
            SettingsFieldUI temp;
            foreach(string s in keys){
                temp=Instantiate<SettingsFieldUI>(SettingsFieldPrefab);
                fields.Add(temp);
                temp.keyField=s;
                temp.valueField=GameManager.UserPreferences.Get(s,"");
                temp.transform.SetParent(contentRoot);
            }
        }
        this.gameObject.SetActive(true);
    }
    public void Cancel(){
        foreach(SettingsFieldUI s in fields){s.Revert();}
        this.gameObject.SetActive(false);
    }
    public void Save(){
        foreach(SettingsFieldUI s in fields){GameManager.UserPreferences.Set(s.keyField,s.Save());}
        GameManager.UserPreferences.SaveFile();
        this.gameObject.SetActive(false);
    }
}
