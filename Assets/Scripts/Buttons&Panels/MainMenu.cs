using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text coinText;//ссылки на панели и текст
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuPanel;

    private void Start()
    {
        int coins = PlayerPrefs.GetInt("Coin");//получение значения количевства монет для изменения их количевства и отображения на екране
        coinText.text = coins.ToString();//перевод значения в строку
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);//запуск сцены с игрой
    }

    public void OpenShop()
    {
        SceneManager.LoadScene(2);//запуск сцены магазина
    }

    public void CloseShop()
    {
        SceneManager.LoadScene(0);//закрытие магазина
        Time.timeScale = 1;
    }

    public void Settings()//открытие панели настроек
    {
        settingsPanel.SetActive(true);//активация панели настроек
        menuPanel.SetActive(false);//деактивация панели меню
    }

    public void BacktoMenu()//закрытие панели настроек
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
