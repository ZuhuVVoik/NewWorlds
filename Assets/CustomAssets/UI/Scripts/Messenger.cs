using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Messenger : MonoBehaviour
{
    public GameObject panel;
    Text message; // ссылка на текст
    static Coroutine RunMessage; // ссылка на запущенную корутину
    private void Start()
    {
        message = GetComponent<Text>();
        // берем компонент текста, т.к. текст и скрипт находятся на одном объекте
        WriteMessage("Ищите энергокристаллы"); // напишите сюда первое сообщение для пользователя

        MainManager.messenger = GetComponent<Messenger>();
    }
    public void WriteMessage(string text) // метод для запуска корутины с выводом сообщения
    {
        if (RunMessage != null) StopCoroutine(RunMessage);
        // проверка и остановка корутины, если она уже была запущена
        panel.SetActive(true);
        this.message.text = ""; // очистка строки
        RunMessage = StartCoroutine(Message(text));
        // запуск корутины с выводом нового сообщения
    }
    IEnumerator Message(string message) // корутина для вывода сообщений
    {
        this.message.text = message; // записываем сообщение
        yield return new WaitForSeconds(4f); // ждем 4 секунды
        this.message.text = ""; // очищаем строку
        panel.SetActive(false);
    }

}
