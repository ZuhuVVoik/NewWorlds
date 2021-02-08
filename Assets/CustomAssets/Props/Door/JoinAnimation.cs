using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinAnimation : MonoBehaviour
{
    public Animator dooranimator;//ссылка на аниматор двери   
    public Transform target;//ссылка на точку для начала анимации
    Quaternion newrot;//требуемый поворот    
    Animator anim;//аниматор персонажа
    bool secondturn = false;
    States state;//текущее состояние    
    enum States//перечисление состояний персонажа
    {
        wait,//ожидание
        turn,//поворот 
        walk//перемещение 
    }
    void Start()
    {
        anim = GetComponent<Animator>();//инициализируем аниматор
        state = States.wait;     //изначально состояние ожидания
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) GoToPoint();
        switch (state)//переключаем в зависимости от состояния
        {
            case States.turn://при повороте к точке
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, newrot, Time.deltaTime * 2);//интерполируем между начальным поворотом и требуемым
                    if (Mathf.Abs(Mathf.Round(newrot.y * 100)) == Mathf.Abs(Mathf.Round(transform.rotation.y * 100)))//проверяем когда персонаж повернулся
                    {
                        transform.rotation = newrot;//для избежания погрешности
                        if (!secondturn)
                        {
                            state = States.walk;//переключаем состояние на перемещение
                            anim.SetBool("IsWalking", true);      //включаем анимацию ходьбы    
                        }
                        else
                        {
                            Debug.Log(anim.applyRootMotion);
                            dooranimator.SetTrigger("Open");//запуск анимации двери
                            anim.SetTrigger("Open");//запуск анимации персонажа
                            secondturn = !secondturn;
                            state = States.wait;
                        }
                    }
                    break;
                }
            case States.walk:
                {
                    anim.applyRootMotion = false;
                    transform.position = transform.position + transform.forward * Time.deltaTime;//перемещаем персонажа прямо                   
                    if (Vector3.Distance(transform.position, target.position) <= 0.5f)//дошел
                    {
                        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);//для исключения погрешности ставим в требуемую точку
                        anim.SetBool("IsWalking", false);//выключаем анимацию ходьбы
                        secondturn = true;
                        state = States.wait;
                        GoToPoint();
                    }
                    break;
                }
        }
    }
    public void GoToPoint()//функция для начала выполнения 
    {
        if (Vector3.Distance(transform.position, target.position) <= 1)
        {
            if (state == States.wait)//если ждем
            {
                state = States.turn;//переходим в состояние поворота к точке
                Vector3 relativePos = new Vector3();
                if (!secondturn)
                {
                    relativePos = target.position - transform.position;//вычисляем координату куда нужно будет повернуться
                }
                else
                {
                    Vector3 forward = target.transform.position + target.transform.forward;
                    relativePos = new Vector3(forward.x, transform.position.y, forward.z) - transform.position;
                }
                newrot = Quaternion.LookRotation(relativePos);//указываем нужный поворот
            }
        }
    }

}

