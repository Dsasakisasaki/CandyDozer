using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefabs;//ゲームオブジェクト型配列
    public Transform candyParentTransform;//親子関係を結ぶため
    public CandyManager candyManager;//キャンディのストック管理と表示するスクリプト
    public float shotForce;
    public float shotTorque;
    public float baseWidth;//baseObjectから幅５を取ってきてもいいが面倒なのでインスペクターから入力できるようにした

    void Update()
    {
        //ボタン設定
        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
        }
    }

    //キャンディのプレハブからランダムに一つ選ぶ
    GameObject SampleCandy()
    {
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
    }

    //キャンディの射出位置の調整
    Vector3 GetInstantiatePosition()
    {
        //画面サイズとinputの割合からキャンディの生成ポジションを計算
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }


    public void Shot()
    {
        //ストック0ならshotしない
        if(candyManager.GetCandyAmount() <= 0)
        {
            return;
        }

        //プレハブからCandyオブジェクトを作成
        GameObject candy = Instantiate(
            SampleCandy(),//作るオブジェクト
            GetInstantiatePosition(),//作る場所
            Quaternion.identity//向き
            );

        //生成したCandyオブジェクトにcandyParentTransformを親として設定する
        candy.transform.parent = candyParentTransform;//ここで代入したものの子要素になる。
        //ここで代入できるのはTransformを持つオブジェクト(全てのゲームオブジェクトはTransformを持っているのでつまり全ては親になれる)

        //CandyオブジェクトのRigidbodyを習得し力と回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward * shotForce);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        //ストックを消費
        candyManager.ConsumeCandy();

    }
}
