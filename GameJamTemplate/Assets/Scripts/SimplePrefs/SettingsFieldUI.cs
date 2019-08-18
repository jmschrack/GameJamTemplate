using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsFieldUI : MonoBehaviour
{
    [SerializeField]
    private Text _keyField;
    public string keyField{
        get{return _keyField.text;}
        set{_keyField.text=value;}
    }
    [SerializeField]
    private InputField _valueField;
    private string original;
    public string valueField{
        get{
            return _valueField.text;
        }
        set{
            _valueField.text=value;
            original=value;
        }
    }
    public void Revert(){
        _valueField.text=original;
    }
    public string Save(){
        original=_valueField.text;
        return original;
    }
}
