using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Animator anim;
    Collider currentCollider;
    float rotateAroundUpAxis, moveForward;
    public int moveSpeed = 10;
    public int rotateSpeed = 100;
    public Text text;
    public Camera shoulderCamera;
    public float cameraOrbitUpLimitAngle = 5.0f;
    public float cameraOrbitDownLimitAngle = -5.0f;

    GameObject inventory;
    GameObject map;
    [HideInInspector]
    public bool isPlayerInputDisabled;
    AudioManager audioManager;

    // Use this for initialization
    enum possibleActions { kill, take, open, nothing };
    possibleActions toDoAction;

    private float mouseYAxisTotal;

    void Start()
    {
        mouseYAxisTotal = 0.0f;
        isPlayerInputDisabled = false;
        inventory = GameObject.Find("Canvas").transform.Find("Inventory").gameObject;
        map = GameObject.Find("Canvas").transform.Find("Map").gameObject;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            // inventory is not active and the map is not on the screen
            if (!inventory.active && !map.active)
            {
                inventory.SetActive(true);
                audioManager.Play("Inventory");
                SetPlayerDisabled(true);
            }
            else if (inventory.active) //if the inventory is active
            {
                inventory.SetActive(false);
                audioManager.Play("Inventory");
                SetPlayerDisabled(false);
            }
        }

        if (!isPlayerInputDisabled)
        {
            float mouseXForCamera = Input.GetAxis("Mouse X");
            float mouseYForCamera = -1.0f * Input.GetAxis("Mouse Y");

            mouseYAxisTotal += mouseYForCamera;
            if (mouseYAxisTotal > cameraOrbitUpLimitAngle)
            {
                mouseYAxisTotal = cameraOrbitUpLimitAngle;
                mouseYForCamera = 0.0f;
            }
            else if (mouseYAxisTotal < cameraOrbitDownLimitAngle)
            {
                mouseYAxisTotal = cameraOrbitDownLimitAngle;
                mouseYForCamera = 0.0f;
            }

            shoulderCamera.transform.RotateAround(gameObject.transform.position, Vector3.up, mouseXForCamera * 2);
            shoulderCamera.transform.Rotate(Vector3.right, mouseYForCamera * 2);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentCollider != null)
                {
                    Debug.Log("Action to do: " + toDoAction);
                    if (toDoAction == possibleActions.open)
                    {
                        //currentCollider.gameObject.GetComponent<Door>().openDoor();
                        //anim.SetTrigger("open");
                    }
                    else if (toDoAction == possibleActions.kill)
                    {
                        currentCollider.gameObject.GetComponent<Animator>().SetTrigger("Die");

                        //anim.SetTrigger("killing");
                    }
                    else if (toDoAction == possibleActions.take)
                    {
                        Destroy(currentCollider.gameObject);
                        Debug.Log("You took " + currentCollider.gameObject.name);
                        //anim.SetTrigger("take");
                        AfterAction();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isPlayerInputDisabled)
        {
            rotateAroundUpAxis = Input.GetAxis("Horizontal");
            moveForward = 1.0f * Input.GetAxis("Vertical");

            transform.Rotate(0, rotateAroundUpAxis * rotateSpeed * Time.fixedDeltaTime, 0);
            transform.Translate(new Vector3(0, 0, moveForward * moveSpeed * Time.fixedDeltaTime));
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "killable")
        {
            text.text = "Press E to kill";
            currentCollider = col;
            toDoAction = possibleActions.kill;
        }
        else if (col.tag == "takeable")
        {
            text.text = "Press E to take " + col.gameObject.name;
            currentCollider = col;
            toDoAction = possibleActions.take;


        }
        else if (col.tag == "openable")
        {
            text.text = "Press E to open the door";
            currentCollider = col;
            toDoAction = possibleActions.open;
        }
    }

    void AfterAction()
    {
        text.text = "";
        currentCollider = null;
        toDoAction = possibleActions.nothing;

    }

    private void OnTriggerExit(Collider other)
    {
        AfterAction();
    }

    public void SetPlayerDisabled(bool value)
    {
        isPlayerInputDisabled = value;
    }
}