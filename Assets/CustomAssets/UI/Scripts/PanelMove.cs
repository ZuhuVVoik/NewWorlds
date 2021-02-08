using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PanelMove : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform UIGameobject; // трансформ UI Панели
    float width; // ширина панели
    float changeX; // значение содержащее смещение панели
    float speedPanel = 80; // скорость закрытия панели
    enum states // перечисление состояний панели
    {
        open, close, opening, closing
    }
    states state = states.close; // изначальное состояние закрытое
    void Start()
    {
        UIGameobject = gameObject.GetComponent<RectTransform>();
        // инициализируем переменную трансформа
        width = UIGameobject.sizeDelta.x;
        // определение ширины панели на которую надо отодвинуться вправо      
    }
    void FixedUpdate()
    {
        if (state == states.closing)
        {
            float x = UIGameobject.anchoredPosition.x;
            float y = UIGameobject.anchoredPosition.y;
            x -= speedPanel;
            changeX += speedPanel;
            UIGameobject.anchoredPosition = new Vector2(x, y);
            if (changeX > width)
            {
                state = states.close;
                changeX = 0;
            }
        }
        if (state == states.opening)
        {
            float x = UIGameobject.anchoredPosition.x;
            float y = UIGameobject.anchoredPosition.y;
            x += speedPanel;
            changeX += speedPanel;
            UIGameobject.anchoredPosition = new Vector2(x, y);
            if (changeX > width)
            {
                state = states.open;
                changeX = 0;
            }
        }
    }
    //по клику на панель, открываем новую сцену
    public void OnPointerClick(PointerEventData eventData)
    {
        MainManager.sceneChanger.OpenFirstScene();
    }
    //при наведении на панель мышью, открываем ее
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (state == states.close) state = states.opening;
    }
    //при отведении мыши закрываем панель
    public void OnPointerExit(PointerEventData eventData)
    {
        if (state == states.open) state = states.closing;
    }
}

