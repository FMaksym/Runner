using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text coinText;//������ �� ������ � �����
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuPanel;

    private void Start()
    {
        int coins = PlayerPrefs.GetInt("Coin");//��������� �������� ����������� ����� ��� ��������� �� ����������� � ����������� �� ������
        coinText.text = coins.ToString();//������� �������� � ������
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);//������ ����� � �����
    }

    public void OpenShop()
    {
        SceneManager.LoadScene(2);//������ ����� ��������
    }

    public void CloseShop()
    {
        SceneManager.LoadScene(0);//�������� ��������
        Time.timeScale = 1;
    }

    public void Settings()//�������� ������ ��������
    {
        settingsPanel.SetActive(true);//��������� ������ ��������
        menuPanel.SetActive(false);//����������� ������ ����
    }

    public void BacktoMenu()//�������� ������ ��������
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
