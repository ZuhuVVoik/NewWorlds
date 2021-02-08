using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakbleBot : MonoBehaviour
{
    /* Скрипт для поворота бота и разговора */

    Vector3 startPos;
    Vector3 endPos;
    Animator animator;
    AudioSource speech;

    public PlayerMove player;

    public float outRotationSpeed = 80f;
    public float inRotationSpeed = 160f;

    bool interact = false;

    // Start is called before the first frame update
    void Start()
    {
        speech = this.GetComponent<AudioSource>();
        startPos = this.transform.position;
        animator = this.GetComponent<Animator>();


        animator.SetBool("bot_flag", true); /* пометим что именно бот */
        animator.SetBool("bot_sitting", true); /* ну и пусть сидит */
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position) < 6)
        {
            endPos = player.transform.position - transform.position;
            animator.SetBool("bot_sitting", false);

            var q = Quaternion.LookRotation(endPos - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, inRotationSpeed * Time.deltaTime);
        }
        else
        {
            endPos = startPos;
            animator.SetBool("bot_sitting", true);

            var q = Quaternion.LookRotation(endPos - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, outRotationSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            StartCoroutine(TalkToPlayer());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }

    IEnumerator TalkToPlayer()
    {
        yield return new WaitForSeconds(1);
        speech.Play();
    }
}
