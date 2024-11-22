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
            SceneManager.LoadScene("–¼‘O“ü—Í‰æ–Ê");
        });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
