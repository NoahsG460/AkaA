using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene3 : MonoBehaviour
{
    public void change_button()	//ボタンが押されると処理を行う判定
    {
        SceneManager.LoadScene("操作説明画面");//charaというシーンに移動する
    }
}
