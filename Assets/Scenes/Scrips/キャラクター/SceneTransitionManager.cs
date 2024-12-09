using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    private bool isTransitioning = false; // �V�[���J�ڒ��t���O

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerSceneTransition(string sceneName, float delay)
    {
        if (!isTransitioning) // ���ɃV�[���J�ڒ��Ȃ牽�����Ȃ�
        {
            isTransitioning = true;
            StartCoroutine(SceneTransitionCoroutine(sceneName, delay));
        }
    }

    private IEnumerator SceneTransitionCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

