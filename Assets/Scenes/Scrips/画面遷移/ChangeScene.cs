using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void change_button()	//�{�^�����������Ə������s������
    {
        SceneManager.LoadScene("CharacterSelect");//chara�Ƃ����V�[���Ɉړ�����
    }
}
