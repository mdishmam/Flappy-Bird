using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;


public class GameHandeller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameHandeller.Start");

        GameObject gameObject = new GameObject("Pipe", typeof(SpriteRenderer));
 //       gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.GetInstance().pipeHeadSprite;


//        FunctionPeriodic.Create(() => {
 //           CMDebug.TextPopupMouse("Ding! " + count);
 //          count++;
 //       }, .300f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
