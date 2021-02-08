using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    Animator anim; //переменная для ссылки на контроллер анимации
    bool interact; // указывает, происходит ли взаимодействие
    Vector3 positionForIК; // позиция объекта для взаимодействия
    //float weight=0;
    float weight;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorIK() // метод подобен Update, но используется для программных анимаций
    {
        if (interact)
        {
            if (weight < 1) weight += 0.5f; // т.к. вес меняется от 0 до 1, а изначально мы задали 0, то для плавного перехода руки от исходной позиции достаточно плавно изменять вес ik анимации.
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight); // заменим 1f на w
            anim.SetIKPosition(AvatarIKGoal.RightHand, positionForIК); //указываем позицию для направления левой руки
            anim.SetLookAtWeight(weight); //сначала устанавливаем вес анимации (1 – полностью перезаписывает существующую анимацию и персонаж будет смотреть ровно на объект
            anim.SetLookAtPosition(positionForIК); //указываем куда нужно смотреть
        }
        else
        {
            if (weight > 0) // добавим это условие для плавного изменения анимации при отдалении
            {
                weight -= 0.02f; // теперь нужно плавно убрать воздействие анимации, вернув влияние на 0
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
                anim.SetIKPosition(AvatarIKGoal.RightHand, positionForIК);
                anim.SetLookAtWeight(weight);
                anim.SetLookAtPosition(positionForIК);
            }
        }
    }


    public void StartInteraction(Vector3 pos)
    {
        positionForIК = pos;
        interact = true;
    }

    public void StopInteraction()
    {
        interact = false;
    }

}
