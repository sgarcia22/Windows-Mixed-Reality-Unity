using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class GrabObject : MonoBehaviour, IFocusable, IInputClickHandler
{
    public GameObject hand, motionControllers;
    private GameObject rightController;
    public Color normalColor, highlightColor;
    private Renderer rend;
    private bool holding = false, initialized = false;
    private Transform controllerTransform;

    void Awake ()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.sharedMaterial.color = normalColor;

    }

    // Use this for initialization
    void Start () {
        StartCoroutine("InitializeVariables");
    }

    IEnumerator InitializeVariables ()
    {
        yield return new WaitForSeconds(3f);
        hand = GameObject.FindGameObjectWithTag("Hand");
        rightController = motionControllers.transform.GetChild(0).gameObject;
        controllerTransform = rightController.transform;
        Debug.Log(rightController);
        hand.transform.parent = rightController.transform;
        initialized = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (initialized && holding)
            controllerTransform = rightController.transform;
        if (holding)
        {
            gameObject.transform.position = hand.transform.position;
        }
    }

    public void OnFocusEnter()
    {
        Debug.Log("Focus Enter");
    }

    public void OnFocusExit()
    {
        Debug.Log("Focus Exit");
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Clicked Controller");
        //Hold the cube
        if (!holding)
        {
            gameObject.transform.position = hand.transform.position;
            holding = true;
        }
        else
        {
            holding = false;
        }
    }

}
