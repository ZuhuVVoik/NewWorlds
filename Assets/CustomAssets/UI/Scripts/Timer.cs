using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] Text text; // ссылка куда выводить текст времени
    private DateTime timer = new DateTime(1, 1, 1, 0, 3, 00); // задаем стартовое время таймера

    void Start()
    {
        StartCoroutine(Timerenumerator());
    }

    IEnumerator Timerenumerator() // корутина будет запускаться 
    {
        while (true)
        {
            
            if(timer.Second.ToString() == "0")
            {
                text.text = timer.Minute.ToString() + ":00"; // вывод в строку
            }
            else
            {
                text.text = timer.Minute.ToString() + ":" + timer.Second.ToString(); // вывод в строку
            }

            timer = timer.AddSeconds(-1); // отнимает от времени одну секунду

            if (timer.Second == 0 && timer.Minute == 0) // когда времени не осталось
            {
                text.text = "Время вышло!"; // пишем, что время вышло
                text.color = new Color(1, 0, 0); // красим текст красным
                MainManager.game.LoseGame(); // вызываем конец игры!
                break; // завершаем корутину
            }
            yield return new WaitForSeconds(1); // ждем секунду
        }
    }

}
