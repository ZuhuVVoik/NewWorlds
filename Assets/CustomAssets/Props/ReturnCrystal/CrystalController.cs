using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    bool movingUp = true;

    public float rotatingSpeed = 500f;
    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = StartCoroutine(ChangeDirection());   
    }

    // Update is called once per frame
    void Update()
    {

        if (movingUp)
        {
            this.transform.Translate(0, Time.deltaTime/4, 0);
        }
        else
        {
            this.transform.Translate(0, -Time.deltaTime/4, 0);
        }
        this.transform.Rotate(0, 5f * Time.deltaTime, 0);
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            movingUp = !movingUp;
            yield return new WaitForSeconds(2);
        }
    }
}
