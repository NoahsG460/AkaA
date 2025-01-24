using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム内に制限時間を「分：秒」で表示するクラス
/// </summary>
public class BattleTime : MonoBehaviour
{

    [SerializeField, Header("制限時間の設定値")]
    public int battleTime;
    [SerializeField, Header("制限時間の表示")]
    public Text battleTimeText;

    private int currentTime;              // 現在の残り時間（不要な場合は宣言しない）
    private float timer;                  // 時間計測用

    void Start()
    {
        // currentTimeを利用する場合にはここで代入する。以下、必要に応じてbattleTimeをcurrentTimeに書き換える
        currentTime = battleTime;
    }

    void Update()
    {
        // timerを利用して経過時間を計測
        timer += Time.deltaTime;
        // 1秒経過ごとにtimerを0に戻し、battleTime(currentTime)を減算する
        if (timer >= 1)
        {
            timer = 0;
            battleTime--;  // あるいは、currentTime--;
            // 時間表示を更新するメソッドを呼び出す
            DisplayBattleTime(battleTime);   // あるいは、DisplayBattleTime(currentTime);
        }
    }

    /// <summary>
    /// 制限時間を更新して[分:秒]で表示する
    /// </summary>
    private void DisplayBattleTime(int limitTime)
    {
        // 引数で受け取った値を[分:秒]に変換して表示する
        // ToString("00")でゼロプレースフォルダーして、１桁のときは頭に0をつける
        battleTimeText.text = ((int)(limitTime / 60)).ToString("00") + ":" + ((int)limitTime % 60).ToString("00");

        if (battleTime == 0)
        {
            SceneManager.LoadScene("リザルト");
        }
    }
}