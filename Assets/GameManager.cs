using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Coroutine end; // ссылка на запущенную корутину, чтобы не проиграть после выигрыша
    public void WinGame() // в случае выигрыша
    {
        if (end == null) // проверяем, была ли уже выиграна или проиграна игра
        {
            MainManager.Messenger.WriteMessage("Поздравляем, вы выиграли!");
            end = StartCoroutine(BeforeExit()); // запускаем окончание игры через 4 секунды
        }
    }
    public void LoseGame() // в случае проигрыша
    {
        if (end == null)
        {
            MainManager.Messenger.WriteMessage("Вы проиграли!");
            end = StartCoroutine(BeforeExit());
        }
    }
    public void ExitGame() // выход из игры
    {
        Application.Quit();
    }

    IEnumerator BeforeExit() // корутина перед выходом для прочтения последних сообщений
    {
        yield return new WaitForSeconds(4f);
        MainManager.sceneChanger.OpenFirstScene(); // выходим в главное меню
        MainManager.ClearData();
    }




    
}
