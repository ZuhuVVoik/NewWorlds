using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    NavMeshAgent botagent; // ссылка на агент навигации
    Animator animbot; // ссылка на аниматор бота
    float weight = 0f;
    [SerializeField]
    GameObject[] points; // массив точек для переходов
    //перечисление состояний бота
    enum states
    {
        waiting, // ожидает
        going, // идёт
        dialog
    }
    states state = states.waiting; // изначальное состояние ожидания

    void Start()
    {
        animbot = GetComponent<Animator>(); // берем компонент аниматора
        botagent = GetComponent<NavMeshAgent>(); // берем компонент агента
        StartCoroutine(Wait()); // запускаем корутину ожидания
    }


    void FixedUpdate()
    {
        switch (state)
        {
            case (states.waiting):
                {
                    if (PlayerNear()) PrepareToDialog();
                    break;
                }
            case states.going:
                {
                    if (PlayerNear()) PrepareToDialog();
                    else if ((Vector3.Distance(transform.position, botagent.destination)) < 3) // если дистанция до пункта назначения меньше заданного расстояния (т.е. бот дошел до выданной ему точки)
                    {
                        StartCoroutine(Wait()); // вызываем корутину ожидания
                    }
                    break;
                }

            case states.dialog:
                {
                    break;
                }
        }
    }

    bool PlayerNear()
    {
        return false;
    }

    void PrepareToDialog()
    {
        botagent.SetDestination(transform.position); // обнуляем точку, чтобы бот никуда не шёл
        animbot.SetBool("IsWalking", false); // останавливаем анимацию ходьбы
        state = states.dialog; // устанавливаем состояние подхода к объекту в который попали лучом        
    }

    void OnAnimatorIK()
    {
        if (state == states.dialog)
        {
            if (weight < 1) weight += 0.1f;
            animbot.SetLookAtWeight(weight); // указываем силу воздействия на голову
            // указываем куда смотреть
        }
        else if (weight > 0)
        {
            weight -= 0.1f;
            animbot.SetLookAtWeight(weight);
        }
    }


    IEnumerator Wait() // корутина ожидания
    {
        botagent.SetDestination(transform.position); // обнуляем точку, чтобы бот никуда не шёл
        animbot.SetBool("IsWalking", false); // останавливаем анимацию ходьбы
        state = states.waiting; // указываем, что бот перешел в режим ожидания

        yield return new WaitForSeconds(1f); // ждем 1 секунд

        botagent.SetDestination(points[Random.Range(0, points.Length)].transform.position);

        // destination – куда идти боту, передаем ему рандомно одну из наших точек
        animbot.SetBool("IsWalking", true); // включаем анимацию ходьбы
        state = states.going; // указываем, что бот находится в движении  
    }

}
