using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishTrigger : MonoBehaviour
{

    public controller Controller;

    private void Start()
    {
        if (Controller == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Controller = player.GetComponent<controller>();
            }

            if (Controller == null)
            {
                Debug.LogError("Controller not assigned and could not be found.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
            Controller.hasFinished = true;

    }

}
