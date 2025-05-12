using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    private GameObject Player;
    private controller RR;
    private GameObject cameralookAt, cameraPos;
    private float speed = 0;
    private float defaltFOV = 0, desiredFOV = 0;
    [Range(0, 50)] public float smothTime = 8;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("No GameObject with tag 'Player' found.");
            return;
        }

        RR = Player.GetComponent<controller>();
        if (RR == null)
        {
            Debug.LogError("Player does not have a 'controller' component.");
            return;
        }

        Transform lookAtTransform = Player.transform.Find("camera lookAt");
        Transform constraintTransform = Player.transform.Find("camera constraint");

        if (lookAtTransform == null || constraintTransform == null)
        {
            Debug.LogError("Missing camera transforms on Player.");
            return;
        }

        cameralookAt = lookAtTransform.gameObject;
        cameraPos = constraintTransform.gameObject;

        defaltFOV = Camera.main.fieldOfView;
        desiredFOV = defaltFOV + 15;
    }

    private void FixedUpdate()
    {
        if (Player == null || RR == null || cameralookAt == null || cameraPos == null)
        {
            Debug.LogWarning("CameraController: Missing or destroyed reference.");
            return;
        }

        follow();
        boostFOV();
    }
    private void follow()
    {
        speed = RR.KPH / smothTime;
        gameObject.transform.position = Vector3.Lerp(transform.position, cameraPos.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(cameralookAt.gameObject.transform.position);
    }
    private void boostFOV()
    {

        if (RR.nitrusFlag)
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, desiredFOV, Time.deltaTime * 5);
        else
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, defaltFOV, Time.deltaTime * 5);

    }

}