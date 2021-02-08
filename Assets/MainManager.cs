using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static Messenger messenger;
    public static SceneChanger sceneChanger;

    public static GameManager game;
    public static Greed greed;

    /* Обязательно наличие контейнера для кристаллов */
    public static bool IsContainerPickedUp = false;

    /* Дополнительное увеличение очков выносливости */
    public static float AdditionalStaminaCount;

    public static Animator playerAnimator;

    static InventoryManager inventory;

    /* Найден ли хоть один предмет типа */
    static bool greenFound = false;
    static bool cyanFound = false;

    public static List<string> searchOrder = new List<string>();

    public static bool IsKilled = false;
    static float healthPoints = 100;
    public static float HealthPoints
    {
        get { return healthPoints; }
        set
        {
            healthPoints = value;
            if(healthPoints < 0)
            {
                IsKilled = true;
                playerAnimator.gameObject.transform.position = new Vector3(playerAnimator.gameObject.transform.position.x, playerAnimator.gameObject.transform.position.y - 0.5f, playerAnimator.gameObject.transform.position.z);
                game.LoseGame();
            }
            else
            {
                playerAnimator.Play("Hit");
            }
        }
    }

    public static bool GreenFound 
    {
        get { return greenFound; }
        set
        {
            greenFound = value;
        }
    }
    public static bool CyanFound
    {
        get { return cyanFound; }
        set
        {
            cyanFound = value;
        }
    }

    /* Количества собираемых предметов */
    static int stdCount;
    static int rareCount;

    public static InventoryManager Inventory
    {
        get
        {
            if (inventory == null)
            {
                inventory = FindObjectOfType<InventoryManager>();
            }
            return inventory;
        }
        private set
        {
            inventory = value;
        }
    }


    public static int StdCount
    {
        get { return stdCount; }
        set
        {
            stdCount = value;
            inventory.AddCrystalStd();
        }
    }
    public static int RareCount
    {
        get { return rareCount; }
        set
        {
            rareCount = value;
            inventory.AddCrystalRare();
        }
    }

    public static Messenger Messenger
    {
        get
        {
            if (messenger == null) // инициализация по запросу
            { messenger = FindObjectOfType<Messenger>(); }
            return messenger;
        }
        private set { messenger = value; }
    }
    private void OnEnable()
    {
        AdditionalStaminaCount = 0;   

        DontDestroyOnLoad(gameObject);
        sceneChanger = GetComponent<SceneChanger>();
        game = GetComponent<GameManager>();
        greed = GetComponent<Greed>();
    }

    public static void ClearData()
    {
        RareCount = 0;
        StdCount = 0;
        searchOrder.Clear();
        cyanFound = false;
        greenFound = false;
        healthPoints = 100;
        MainManager.IsKilled = false;
    }

}

