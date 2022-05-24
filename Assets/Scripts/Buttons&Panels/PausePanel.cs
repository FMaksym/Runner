using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;//������ �� ������
    [SerializeField] private GameObject settingsPanel;

    public void PauseLevel()
    {
        pausePanel.SetActive(true);//��������� ������ �����
        Time.timeScale = 0;
    }

    public void ResumeLevel()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Settings()//�� �� ������ ��� � � MainMenu
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void BacktoPause()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
