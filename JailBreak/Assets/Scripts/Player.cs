using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    List<string> inventoryList = new List<string>();
    public int moveSpeed = 10;
    public int rotateSpeed = 5000;
    public Camera shoulderCamera;
    public float cameraOrbitUpLimitAngle = 5.0f;
    public float cameraOrbitDownLimitAngle = -5.0f;
    float rotateAroundUpAxis, moveForward;
    Animator anim;
    Collider currentCollider;
    float movex, movez;
    public int speed = 10;
    public Text text, inventoryText;
    // Use this for initialization
    enum possibleActions { kill, take, open, nothing };
    possibleActions toDoAction;
    private float mouseYAxisTotal;
    void Start()
    {
        mouseYAxisTotal = 0.0f;
        anim = GetComponent<Animator>();
        inventoryText.text = "Invetory :";

    }

    private void Update()
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
                    //if (inventoryList.Contains("Keys")) currentCollider.gameObject.GetComponent<Door>().openDoor();
                   // else text.text = "You need a key to open this door";
                    //anim.SetTrigger("open");
                }
                else if (toDoAction == possibleActions.kill)
                {
                    currentCollider.gameObject.GetComponent<Animator>().SetTrigger("Die");

                    //anim.SetTrigger("killing");
                }
                else if (toDoAction == possibleActions.take)
                {

                    Debug.Log("You took " + currentCollider.gameObject.name);
                    inventoryList.Add(currentCollider.gameObject.name);

                    inventoryText.text += currentCollider.gameObject.name + ", ";
                    //anim.SetTrigger("take");
                    Destroy(currentCollider.gameObject);
                    AfterAction();
                }


            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rotateAroundUpAxis = Input.GetAxis("Horizontal");
        moveForward = 1.0f * Input.GetAxis("Vertical");

        transform.Translate(new Vector3(0, 0, moveForward * moveSpeed * Time.fixedDeltaTime));
        transform.Rotate(0, rotateAroundUpAxis * rotateSpeed * Time.fixedDeltaTime, 0);

        transform.Translate(new Vector3(movex * speed * Time.deltaTime, 0, movez * speed * Time.deltaTime));
        //  Debug.Log(movez);
        if (movez > 0.4f)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    void OnTriggerEnter(Collider col)
    {

        Debug.Log(col.gameObject.name);
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

}
