using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Candy")
        {
            //candyストックを増やす
            candyManager.AddCandy(reward);
            //オブジェクトを削除
            Destroy(other.gameObject);
        }
    }
}
