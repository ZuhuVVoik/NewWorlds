using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
    [SerializeField]
    GameObject objectInScene; // соответствующий объект на сцене 

    public string CrystalType;

    public bool State { get; set; } // автоматич свойство состояние подобран/не подобран этот объект

    void OnEnable()
    {
        objectInScene = this.gameObject;
    }

    public void UpdateImage() // обновить количество кристаллов
    {
        if (State) // если объект активен (подобран)
        {
            if(CrystalType == "rare")
            {
                MainManager.RareCount++;
            }
            if(CrystalType == "std")
            {
                MainManager.StdCount++;
            }
        }
    }

    public GameObject myObject()
    {
        return objectInScene;
    }

}