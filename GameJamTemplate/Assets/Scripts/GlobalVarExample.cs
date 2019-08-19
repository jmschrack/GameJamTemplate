using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVarExample : MonoBehaviour
{
    public void TestWinGame(){
        GameManager.SetGlobalVar("GameEnd","Win");
        GameManager.LoadLevel(2);
    }
    public void TestLoseGame(){
        GameManager.SetGlobalVar("GameEnd","Lose");
        GameManager.LoadLevel(2);
    }
}
