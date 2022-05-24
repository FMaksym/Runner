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
        RecordScore();//вызов метода для проверки рекорда
    }

    public void RestartLevel() 
    {
        SceneManager.LoadScene(1);//перезагрузка сцены уровня
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);//загрузка меню
        Time.timeScale = 1;// задаем значение времени 1 для работы анимаций на сцене
    }

    public void OpenShop()
    {
        SceneManager.LoadScene(2);//загрузка сцены с магазином
    }

    public void RecordScore() 
    {
        int lastScore = PlayerPrefs.GetInt("lastScore");
        int recordScore = PlayerPrefs.GetInt("recordScore");

        if (lastScore > recordScore)//проверка набраных очков и действующего рекода
        {
            recordScore = lastScore;//присваиваем рекорду последнее значение
            PlayerPrefs.SetInt("recordScore", recordScore);
            recordText.text = recordScore.ToString();//переводим рекорд в строку для вывода на экран
        }
        else
        {
            recordText.text = recordScore.ToString();//оставляем текущий рекорд и выводим на экран
        }
    }
}
