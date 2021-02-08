using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greed : MonoBehaviour
{
    /*
     *  Система жадности
     *  Влияет на агрессивность бота при сборе кристаллов
     *  В этом модуле определяется ценность собранных кристаллов и влияние времени и порядка сбора
     */

    float delay = 5f;  // Время до сброса счетчика
    float countingLasts = 5f;

    float preStartGreed = 0.5f; // Очки за кристаллы в инвентаре до старта
    float incomeGreed = 1f; // Очки за поднятый кристалл

    static int currentCollectedCount = 0; // Количество собранных до кулдауна кристаллов
    static int preSceneCount; // Количество кристаллов до запуска уровня

    public float currentGreed; //Текущее количество очков

    static bool isGreedCounting;

    float greedMultiplier = 1.5f; // Множитель при быстром сборе

    void Start()
    {
        preSceneCount = (MainManager.RareCount + MainManager.StdCount);
        currentGreed = preSceneCount * preStartGreed;
    }

    public void OnCrystalPickup()
    {
        if (isGreedCounting)    
        {
            currentCollectedCount = 1;
            
            currentGreed += incomeGreed;

            StartCoroutine(GreedCounting());
        }
        else
        {
            currentGreed += incomeGreed * (float)Math.Pow(greedMultiplier,currentCollectedCount);   // Увеличиваем прирост очков за собранные подряд кристаллы

            currentCollectedCount++;
        }
    }

    IEnumerator GreedCounting()
    {
        isGreedCounting = true;
        while (true)
        {
            countingLasts -= 1f; // отнимает от времени одну секунду

            if (countingLasts == 0) // когда времени не осталось
            {
                currentGreed = preStartGreed + incomeGreed * currentCollectedCount; // Убираем повышенные ставки с очков
                isGreedCounting = false;
                countingLasts = delay;
                break; // завершаем корутину
            }
            yield return new WaitForSeconds(1); // ждем секунду
        }
    }

    void Update()
    {
        
    }
}
