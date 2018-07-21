using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenController : MonoBehaviour {

    public List<GameObject> activeTokenList;
    public List<GameObject> inactiveTokenList;



    private void Start()
    {
   
        

    }


    /// <summary>
    /// Query the token class for available attacks
    /// </summary>
    public bool RequestLightAttack()
    {

        if (activeTokenList.Count > 0)
        {
            //pass token to AI
            inactiveTokenList.Add(activeTokenList[0]);
            activeTokenList.Remove(activeTokenList[0]);
            return true;
        }

        else
        {
            //no tokens available
            print("no tokens available");
            return false;
       
        }


    }

    /// <summary>
    /// 0 = Light Attack Token
    /// </summary>
    /// <param name="value"></param>
    public void ReturnTokens(int value)
    {
        activeTokenList.Add(inactiveTokenList[0]);
        inactiveTokenList.Remove(inactiveTokenList[0]);
    }
    




}
