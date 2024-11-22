using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartBotton : MonoBehaviour
{
    public string SceneName;


    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("名前入力画面");
        });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
