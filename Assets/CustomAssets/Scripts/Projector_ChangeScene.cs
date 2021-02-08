using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector_ChangeScene : MonoBehaviour
{
    public IKHandController playerHand;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(playerHand.inHand != null)
        {
            MainManager.sceneChanger.OpenCaveScene();
        }
    }
}
