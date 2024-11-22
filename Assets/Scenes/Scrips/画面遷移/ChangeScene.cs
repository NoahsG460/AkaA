using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void change_button()	//ボタンが押されると処理を行う判定
    {
        SceneManager.LoadScene("CharacterSelect");//charaというシーンに移動する
    }
}
