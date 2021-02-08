using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemBehaviour : MonoBehaviour
{
    NavMeshAgent botagent; // ссылка на агент навигации
    Animator animbot; // ссылка на аниматор бота
    public GameObject Player;// ссылка на игрока
    AudioSource audio;

    bool chasePlayer = false;
    bool isPlayerInAttackRange = false;

    float attackDelay = 2f;
    float attackDistance = 3f;

    bool attackOnDelay = false;

    float rotationSpeed = 50f;

    /* Шансы анимаций атаки */
    float atkFirstChance = 0.5f;
    float atkSecondChance = 0.5f;

    Coroutine moving;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMove>().gameObject;
        animbot = GetComponent<Animator>(); // берем компонент аниматора
        botagent = GetComponent<NavMeshAgent>(); // берем компонент агента
        audio = GetComponent<AudioSource>();

        moving = StartCoroutine(Moving());
    }

    // Update is called once per frame
    void Update()
    {
        if (chasePlayer == true)
        {
            StopCoroutine(Moving());   
            MoveToPlayer();
            if (CanAttack())
            {
                AttackPlayer();
            }
        }
        else
        {
            RandomMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            CheckGreed();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            CheckGreed();
        }
    }

    public void CheckGreed()
    {
        Debug.Log(chasePlayer);
        if(MainManager.greed.currentGreed > 1)
        {
            chasePlayer = true;
        }
        else
        {
            chasePlayer = false;
        }
    }
    public void MoveToPlayer()
    {
        if (!MainManager.IsKilled)
        {
            botagent.SetDestination(Player.transform.position);
            if ((Vector3.Distance(transform.position, botagent.destination)) < 3)
            {
                animbot.SetBool("IsWalking", false);
                animbot.SetBool("IsRunning", false);


                Vector3 targetDirection = Player.transform.position - transform.position;
                float singleStep = rotationSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
            else
            {
                animbot.SetBool("IsWalking", true);

                if (Vector3.Distance(transform.position, botagent.destination) > 10)
                {
                    animbot.SetBool("IsRunning", true);
                }

                Vector3 targetDirection = Player.transform.position - transform.position;
                float singleStep = rotationSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
            
        }
        else
        {
            botagent.destination = botagent.transform.position;
        }
    }
    public void RandomMove()
    {
        if ((Vector3.Distance(transform.position, botagent.destination)) < 3)
        {
            StartCoroutine(Moving());
        }
    }
    IEnumerator Moving() // корутина ожидания
    {
        botagent.SetDestination(transform.position); // обнуляем точку, чтобы бот никуда не шёл
        animbot.SetBool("IsWalking", false); // останавливаем анимацию ходьбы

        yield return new WaitForSeconds(2f); // ждем 2 секунд

        botagent.SetDestination(RoomsPlacer.CrystalPlaces[Random.Range(0, RoomsPlacer.CrystalPlaces.Length)].transform.position);
        // destination – куда идти боту, передаем ему рандомно одну из наших точек
        animbot.SetBool("IsWalking", true); // включаем анимацию ходьбы
    }

    public bool CanAttack()
    {
        if(Vector3.Distance(Player.transform.position, transform.position) <= attackDistance && !attackOnDelay)
        {
            return true;
        }
        return false;
    }
    public void AttackPlayer()
    {
        if (!attackOnDelay)
        {
            if (!MainManager.IsKilled)
            {
                MainManager.HealthPoints -= 40;
                Debug.Log(MainManager.HealthPoints);
                botagent.destination = botagent.transform.position;

                float chance = Random.value;
                if (chance > atkFirstChance)
                {
                    animbot.Play("Mutant Punch");
                }
                else
                {
                    animbot.Play("Mutant Swiping");
                }
                audio.Play();
                StartCoroutine(AttackDelay());
            }
            else
            {
                botagent.SetDestination(RoomsPlacer.CrystalPlaces[Random.Range(0, RoomsPlacer.CrystalPlaces.Length)].transform.position);
            }
        }
    }
    
    /* Делей на атаку */
    public IEnumerator AttackDelay()
    {
        attackOnDelay = true;
        yield return new WaitForSeconds(attackDelay);
        attackOnDelay = false;
    }
}
