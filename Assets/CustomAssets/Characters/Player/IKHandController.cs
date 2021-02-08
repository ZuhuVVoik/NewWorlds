using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandController : MonoBehaviour
{
    //public Transform interactObject; // объект для взаимодействия

    //[SerializeField]
    //IKAnimation playerIK; // ссылка на экземпляр скрипта IKAnimation
    //Transform inHand;
    //Animator anim; //переменная для ссылки на контроллер анимации
    //bool interact; // указывает, происходит ли взаимодействие
    //Vector3 positionForIК; // позиция объекта для взаимодействия
    //float weight;

    //void OnAnimatorIK() // метод подобен Update, но используется для программных анимаций
    //{
    //    if (interact)
    //    {
    //        if (weight < 1)
    //        { 
    //            weight += 0.01f; // т.к. вес меняется от 0 до 1, а изначально мы задали 0, то для плавного перехода руки от исходной позиции достаточно плавно изменять вес ik анимации.
    //        }
    //        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight); // заменим 1f на w
    //        anim.SetIKPosition(AvatarIKGoal.RightHand, positionForIК);
    //        //указываем позицию для направления левой руки
    //        anim.SetLookAtWeight(weight);
    //        //сначала устанавливаем вес анимации (1 – полностью перезаписывает существующую анимацию и персонаж будет смотреть ровно на объект
    //        anim.SetLookAtPosition(positionForIК); //указываем куда нужно смотреть
    //    }

    //    else if (weight > 0) // добавим это условие для плавного изменения анимации при отдалении
    //    {
    //        weight -= 0.02f; // теперь нужно плавно убрать воздействие анимации, вернув влияние на 0
    //        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
    //        anim.SetIKPosition(AvatarIKGoal.RightHand, positionForIК);
    //        anim.SetLookAtWeight(weight);
    //        anim.SetLookAtPosition(positionForIК);
    //    }
    //}

    //private void OnTriggerEnter(Collider other) // рука попадает в триггер
    //{
    //    if (other.CompareTag("item") || other.CompareTag("itemForTransfer"))
    //    // если у триггера один из этих тегов
    //    {
    //        Debug.Log(other.name);
    //        interactObject = other.transform; // записываем объект для взаимодействия
    //        playerIK.StartInteraction(other.gameObject.transform.position); // сообщаем скрипту IKAnimation о начале взаимодействия для запуска IK - анимации
    //    }
    //}


    //void TakeItemInPocket(GameObject item)
    //{
    //    playerIK.StopInteraction(); // прекращение IK-анимации
    //    Destroy(interactObject.gameObject); // удалить объект
    //}

    //private void FixedUpdate()
    //{
    //    CheckDistance(); // проверка дистанции с объектом
    //}
    //void CheckDistance() // метод для проверки дистанции, чтобы была возможность прекратить взаимодействие с объектом при отдалении
    //{
    //    if (interactObject != null && Vector3.Distance(transform.position, interactObject.position) > 10)
    //    // если происходит взаимодействие и дистанция стала больше 2-ух
    //    {
    //        interactObject = null; // обнуляем ссылку на объект
    //        playerIK.StopInteraction(); // прекращаем IK-анимацию
    //    }
    //}

    //private void OnCollisionEnter(Collision collision) // при коллизии с коллайдером предмета 
    //{
    //    if (collision.gameObject.CompareTag("itemForTransfer") && !inHand)
    //    // если это объект для перемещения и в руке нет другого предмета
    //    {
    //        TakeItemInHand(collision.gameObject.transform);
    //    }

    //}
    //void TakeItemInHand(Transform item) // добавим метод для переноса объекта
    //{
    //    inHand = item; // запоминаем объект для взаимодействия
    //    inHand.parent = transform; // делаем руку, родителем объекта
    //    inHand.localPosition = new Vector3(0, 0, 0); // устанавливаем положение
    //    inHand.localEulerAngles = new Vector3(0, 0, 0); // устанавливаем поворот
    //    playerIK.StopInteraction(); // останавливаем IK-анимацию

    //    MainManager.Messenger.WriteMessage("Вы подобрали " + item.name);
    //}
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(1)) { ThroughItem(); }
    //}
    //void ThroughItem()
    //{
    //    if (inHand != null) // если персонаж держит объект
    //    {
    //        inHand.parent = null; // отвязываем объект      
    //        StartCoroutine(ReadyToTake()); // запускаем корутину
    //    }
    //}
    //IEnumerator ReadyToTake()
    //{
    //    yield return null; // ждем один кадр
    //    inHand = null; // обнуляем ссылку
    //}


    //Transform interactObject; // объект для взаимодействия

    //[SerializeField]
    //IKAnimation playerIK; // ссылка на экземпляр скрипта IKAnimation
    //bool inHand;

    //public GameObject box;

    //private void OnTriggerEnter(Collider other) // рука попадает в триггер
    //{
    //    if (other.CompareTag("item") || other.CompareTag("itemForTransfer"))
    //    // если у триггера один из этих тегов
    //    {
    //        interactObject = other.transform; // записываем объект для взаимодействия
    //        playerIK.StartInteraction(other.gameObject.transform.position); // сообщаем скрипту
    //                                                                        // IKAnimation о начале взаимодействия для запуска IK - анимации
    //    }
    //}
    //private void FixedUpdate()
    //{
    //    CheckDistance(); // проверка дистанции с объектом
    //}
    //void CheckDistance() // метод для проверки дистанции, чтобы была возможность прекратить взаимодействие с объектом при отдалении
    //{
    //    if (interactObject != null && Vector3.Distance(transform.position, interactObject.position) > 2)
    //    // если происходит взаимодействие и дистанция стала больше 2-ух
    //    {
    //        interactObject = null; // обнуляем ссылку на объект
    //        playerIK.StopInteraction(); // прекращаем IK-анимацию
    //    }
    //}

    //private void OnCollisionEnter(Collision collision) // при коллизии с коллайдером предмета
    //{
    //    if (collision.gameObject.CompareTag("item")) // только объекты с тегом item будем удалять
    //    {
    //        TakeItemInPocket(collision.gameObject); // передаем в метод объект для удаления
    //    }
    //    if (collision.gameObject.CompareTag("itemForTransfer") && !inHand)
    //    // если это объект для перемещения и в руке нет другого предмета
    //    {
    //        TakeItemInHand(collision.gameObject.transform);
    //    }
    //}

    //void TakeItemInPocket(GameObject item)
    //{
    //    playerIK.StopInteraction(); // прекращение IK-анимации
    //    Destroy(interactObject.gameObject); // удалить объект
    //}

    //void TakeItemInHand(Transform item) // добавим метод для переноса объекта
    //{
    //    //DeleteRigidbody();
    //    interactObject = item; // запоминаем объект для взаимодействия
    //    interactObject.parent = transform; // делаем руку, родителем объекта
    //    interactObject.localPosition = new Vector3(-0.016f, 0.29f, 0.022f);//устанавливаем позицию
    //    interactObject.localEulerAngles = new Vector3(0f, 0f, 0f);//устанавливаем поворот
    //    inHand = true; // указываем, что в руке есть предмет
    //    playerIK.StopInteraction(); // останавливаем IK-анимацию
    //}
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(1)) { ThroughItem(); }
    //}
    //void DeleteRigidbody()
    //{
    //    UnityEngine.Object.Destroy(box.GetComponent<Rigidbody>());
    //}
    //void ThroughItem()
    //{
    //    if (inHand) // если персонаж держит объект
    //    {
    //        box.transform.parent = null; // отвязываем объект
    //        box.AddComponent<Rigidbody>();
    //        StartCoroutine(ReadyToTake()); // запускаем корутину
    //    }
    //}
    //IEnumerator ReadyToTake()
    //{
    //    yield return null; // ждем один кадр
    //    inHand = false; // обнуляем ссылку
    //}


    Transform interactObject; // объект для взаимодействия
    public Transform inHand;

    AudioSource source;

    [SerializeField]
    IKAnimation playerIK; // ссылка на экземпляр скрипта IKAnimation
    public void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) { ThroughItem(); }
    }
    private void OnTriggerEnter(Collider other) // рука попадает в триггер
    {
        if (other.CompareTag("item") || other.CompareTag("itemForTransfer"))
        // если у триггера один из этих тегов
        {
            interactObject = other.transform; // записываем объект для взаимодействия
            playerIK.StartInteraction(other.gameObject.transform.position); // сообщаем скрипту 
            //IKAnimation о начале взаимодействия для запуска IK - анимации
        }
    }
    private void FixedUpdate()
    {
        CheckDistance(); // проверка дистанции с объектом
    }
    void CheckDistance() // метод для проверки дистанции, чтобы была возможность прекратить взаимодействие с объектом при отдалении
    {
        if (interactObject != null && Vector3.Distance(transform.position, interactObject.position) > 7)
        // если происходит взаимодействие и дистанция стала больше 2-ух
        {
            interactObject = null; // обнуляем ссылку на объект
            playerIK.StopInteraction(); // прекращаем IK-анимацию
        }
    }

    void TakeItemInPocket(GameObject item)
    {
        playerIK.StopInteraction(); // прекращение IK-анимации
        Destroy(interactObject.gameObject); // удалить объект
    }
    void TakeItemInHand(Transform item) // добавим метод для переноса объекта
    {
        inHand = item; // запоминаем объект для взаимодействия
        inHand.parent = transform; // делаем руку, родителем объекта
        inHand.localPosition = new Vector3(0, 0, 0); // устанавливаем положение
        inHand.localEulerAngles = new Vector3(0, 0, 0); // устанавливаем поворот
        playerIK.StopInteraction(); // останавливаем IK-анимацию
    }

    private void OnCollisionEnter(Collision collision) // при коллизии с коллайдером предмета 
    {
        if (collision.gameObject.CompareTag("item")) // только объекты с тегом item будем удалять
        {
            
            if (MainManager.IsContainerPickedUp)
            {
                MainManager.messenger.WriteMessage("Вы подобрали кристалл");
                TakeItemInPocket(collision.gameObject); // передаем в метод объект для удаления
                UIObject obj = collision.gameObject.GetComponent<UIObject>();
                MainManager.Inventory.AddCrystal(obj);
                source.Play();
            }
            if (collision.gameObject.name == "container")
            {
                MainManager.messenger.WriteMessage("Вы подобрали контейнер для кристаллов");
                TakeItemInPocket(collision.gameObject);
                MainManager.IsContainerPickedUp = true;
            }
        }
        if (collision.gameObject.CompareTag("itemForTransfer") && !inHand)
        // если это объект для перемещения и в руке нет другого предмета
        {
            TakeItemInHand(collision.gameObject.transform);
        }

    }
    void ThroughItem()
    {
        if (inHand != null) // если персонаж держит объект
        {
            inHand.parent = null; // отвязываем объект      
            StartCoroutine(ReadyToTake()); // запускаем корутину
        }
    }
    IEnumerator ReadyToTake()
    {
        yield return null; // ждем один кадр
        inHand = null; // обнуляем ссылку
    }




}
