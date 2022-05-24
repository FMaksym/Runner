using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] Text recordText;

    private void Start()
    {
        RecordScore();//����� ������ ��� �������� �������
    }

    public void RestartLevel() 
    {
        SceneManager.LoadScene(1);//������������ ����� ������
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);//�������� ����
        Time.timeScale = 1;// ������ �������� ������� 1 ��� ������ �������� �� �����
    }

    public void OpenShop()
    {
        SceneManager.LoadScene(2);//�������� ����� � ���������
    }

    public void RecordScore() 
    {
        int lastScore = PlayerPrefs.GetInt("lastScore");
        int recordScore = PlayerPrefs.GetInt("recordScore");

        if (lastScore > recordScore)//�������� �������� ����� � ������������ ������
        {
            recordScore = lastScore;//����������� ������� ��������� ��������
            PlayerPrefs.SetInt("recordScore", recordScore);
            recordText.text = recordScore.ToString();//��������� ������ � ������ ��� ������ �� �����
        }
        else
        {
            recordText.text = recordScore.ToString();//��������� ������� ������ � ������� �� �����
        }
    }
}
