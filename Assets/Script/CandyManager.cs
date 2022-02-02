using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 10;

    //現在のキャンディストック数
    public int candy = DefaultCandyAmount;

    //ストック回復までの残り秒数
    int counter;

    public void ConsumeCandy()
    {
        if(candy > 0)
        {
            candy--;
        }
    }

    //ゲッター
    public int GetCandyAmount()
    {
        return candy;
    }

    //キャンディ追加
    public void AddCandy(int amount)
    {
        candy += amount;
    }

    //表示
    private void OnGUI()//
    {
        GUI.color = Color.black;

        //ストック数の表示
        string label = $"Candy : {candy}";

        //回復カウント時だけ秒数を表示
        if (counter > 0)
        {
            label = $"{label}({counter}s)";

        }
        
        GUI.Label(new Rect(50, 50, 100, 30), label);
    }

    private void Update()
    {
        //ストックがデフォルトより少なく、回復カウントしてないときカウントスタート
        if(candy< DefaultCandyAmount && counter <= 0)
        {
            StartCoroutine(RecoverCandy());
        }
    }

    IEnumerator RecoverCandy()
    {
        counter = RecoverySeconds;

        //1秒ずつカウント進める
        while(counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }

        candy++;
    }
}
