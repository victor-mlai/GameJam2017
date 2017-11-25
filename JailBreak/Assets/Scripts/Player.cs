using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Animator anim;
    Collider currentCollider;
    float movex, movez;
    public int speed = 10;
    public Text text;

    // Use this for initialization
    enum possibleActions { kill, take, open, nothing };
    possibleActions toDoAction;

    void Start()
    {

    }

    private void Update()
    {
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
                    afterAction();
                }


            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movex = Input.GetAxis("Horizontal");
        movez = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(movex * speed * Time.deltaTime, 0, movez * speed * Time.deltaTime));
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

    void afterAction()
    {
        text.text = "";
        currentCollider = null;
        toDoAction = possibleActions.nothing;

    }
    private void OnTriggerExit(Collider other)
    {
        afterAction();

    }

}