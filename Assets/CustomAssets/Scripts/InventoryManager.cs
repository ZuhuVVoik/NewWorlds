using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Inventory; // ссылка на панель с инвентарём

    [SerializeField]
    Image stdInvInfo;
    [SerializeField]
    Image rareInvInfo;

    [SerializeField]
    Image[] Placeholders;


    private void Start()
    {
        Inventory.SetActive(false); // скрываем инвентарь

        if (MainManager.StdCount == 0)
        {
            foreach (Transform child in stdInvInfo.transform)
            {
                child.gameObject.SetActive(false);
            }
            stdInvInfo.enabled = false;
        }
        if (MainManager.RareCount == 0)
        {
            foreach (Transform child in rareInvInfo.transform)
            {
                child.gameObject.SetActive(false);
            }
            rareInvInfo.enabled = false;
        }

        if (MainManager.GreenFound == true)
        {
            if (MainManager.searchOrder[0] != "green")
            {
                stdInvInfo.rectTransform.position = new Vector3(stdInvInfo.rectTransform.position.x + 515, stdInvInfo.rectTransform.position.y);
            }
            foreach (Transform child in stdInvInfo.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        if (MainManager.CyanFound == true)
        {
            if (MainManager.searchOrder[0] != "cyan")
            {
                rareInvInfo.rectTransform.position = new Vector3(rareInvInfo.rectTransform.position.x + 515, rareInvInfo.rectTransform.position.y);
            }
            foreach (Transform child in rareInvInfo.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        CheckPlaceholders();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // отслеживаем нажатие клавиши “I”
        {
            Inventory.SetActive(!Inventory.activeSelf); // инвертируем состояние инвентаря
        }


        if(MainManager.StdCount != 0)
        {

            stdInvInfo.enabled = true;
            foreach (Transform child in stdInvInfo.transform)
            {
                child.gameObject.SetActive(true);
            }
            if (MainManager.GreenFound == false)
            {
                if (MainManager.searchOrder[0] != "green")
                {
                    stdInvInfo.rectTransform.position = new Vector3(stdInvInfo.rectTransform.position.x + 515, stdInvInfo.rectTransform.position.y);
                }
            }
            List<Text> textFields = new List<Text>();
            foreach (Transform child in stdInvInfo.transform)
            {
                if (child.GetComponent<Text>())
                {
                    textFields.Add(child.GetComponent<Text>());
                }
            }
            textFields[2].text = "Количество: ";
            textFields[3].text = MainManager.StdCount.ToString();
            MainManager.GreenFound = true;
            CheckPlaceholders();
        }

        if (MainManager.RareCount != 0)
        {
            rareInvInfo.enabled = true;
            foreach (Transform child in rareInvInfo.transform)
            {
                child.gameObject.SetActive(true);
            }
            if (MainManager.CyanFound == false)
            {
                if (MainManager.searchOrder[0] != "cyan")
                {
                    rareInvInfo.rectTransform.position = new Vector3(rareInvInfo.rectTransform.position.x + 515, rareInvInfo.rectTransform.position.y);
                }

            }
            List<Text> textFields = new List<Text>();
            foreach (Transform child in rareInvInfo.transform)
            {
                if (child.GetComponent<Text>())
                {
                    textFields.Add(child.GetComponent<Text>());
                }
            }
            textFields[2].text = "Количество: ";
            textFields[3].text = MainManager.RareCount.ToString();
            MainManager.CyanFound = true;
            CheckPlaceholders();
        }
    }
    public void AddCrystalRare()
    {
        List<Text> textFields = new List<Text>();
        foreach (Transform child in rareInvInfo.transform)
        {
            if (child.GetComponent<Text>())
            {
                textFields.Add(child.GetComponent<Text>());
            }
        }
        textFields[2].text = "Количество: ";
        textFields[3].text = MainManager.RareCount.ToString();

        if (!MainManager.searchOrder.Contains("cyan") && !MainManager.CyanFound)
        {
            MainManager.searchOrder.Add("cyan");
        }
    }
    public void AddCrystalStd()
    {
        List<Text> textFields = new List<Text>();
        foreach (Transform child in stdInvInfo.transform)
        {
            if (child.GetComponent<Text>())
            {
                textFields.Add(child.GetComponent<Text>());
            }
        }
        textFields[2].text = "Количество: ";
        textFields[3].text = MainManager.StdCount.ToString();




        if (!MainManager.searchOrder.Contains("green") && !MainManager.GreenFound)
        {
            MainManager.searchOrder.Add("green");
        }
    }

    public void AddCrystal(UIObject @object)
    {
        if(@object.CrystalType == "rare")
        {
            MainManager.RareCount++;
        }
        if(@object.CrystalType == "std")
        {
            MainManager.StdCount++;
        }
        MainManager.greed.OnCrystalPickup();

    /*       _________________________________________
     *      |                                         |
     *      |        Временный метод на победу        | 
     *      |_________________________________________|
     */
        if (CheckItems()) MainManager.game.WinGame();
    }


    /* Убираем плейсхолдеры */
    void CheckPlaceholders()
    {
        for(int i = 0; i < MainManager.searchOrder.Count; i++)
        {
            Placeholders[i].enabled = false;
        }
    }
    


    //Вовик перепиши потом это чекер а то тут говно
    bool CheckItems() // проверка, все ли объекты собраны
    {
        if (MainManager.StdCount > 20) return true;
        if (MainManager.RareCount > 4) return true;
        return false;
    }

    

}
