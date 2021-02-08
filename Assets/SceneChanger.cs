using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    IEnumerator AsyncLoad(int index)
    {
        AsyncOperation ready = null;
        ready = SceneManager.LoadSceneAsync(index);
        while (!ready.isDone) // пока сцена не загрузилась
        {
            yield return null; // ждём следующий кадр
        }
    }
    public void OpenFirstScene() // метод для смены сцены
    {
        int index = SceneManager.GetActiveScene().buildIndex; // берем индекс запущенной сцены
        if (index == 0) index = 1; // меняем индекс с 0 на 1 или с 1 на 0
        else index = 0;
        StartCoroutine(AsyncLoad(index)); // запускаем асинхронную загрузку сцены
    }

    public void OpenCaveScene()
    {
        StartCoroutine(AsyncLoad(2));
    }
    public void OpenSafezoneScene()
    {
        StartCoroutine(AsyncLoad(1));
    }

}
