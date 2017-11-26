using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questioning : MonoBehaviour {

    // TODO sounds when someone dies
    // TODO sounds when picking up a key or using it
    // TODO replace map in canvas
    // TODO complete questions

    Text question;
    Text choice1;
    Text choice2;
    Text choice3;

    GameObject b_question;
    GameObject b_choice1;
    GameObject b_choice2;
    GameObject b_choice3;

    Player player;

    int currentQuestion = 1;

    int[] satisfaction = new int[3];

    // Use this for initialization
    void Start ()
    {
        b_question = gameObject.transform.Find("Question").gameObject;
        b_choice1 = gameObject.transform.Find("Choice 1").gameObject;
        b_choice2 = gameObject.transform.Find("Choice 2").gameObject;
        b_choice3 = gameObject.transform.Find("Choice 3").gameObject;


        question = gameObject.transform.Find("Question").Find("Text").gameObject.GetComponent<Text>();
        choice1 = gameObject.transform.Find("Choice 1").Find("Text").gameObject.GetComponent<Text>();
        choice2 = gameObject.transform.Find("Choice 2").Find("Text").gameObject.GetComponent<Text>();
        choice3 = gameObject.transform.Find("Choice 3").Find("Text").gameObject.GetComponent<Text>();

        player = GameObject.Find("Player").gameObject.GetComponent<Player>();

        Question1();
    }

    // Update is called once per frame
    void Update () {
        
    }

    public void Question(string question, string choice1, string choice2, string choice3,
        int sat1, int sat2, int sat3)
    {
        /*this.question.text = "Why are you here?";
        this.choice1.text = "I wanted to clean the room";
        this.choice2.text = "It's none of your business";
        this.choice3.text = "I forgot my wallet";*/

        this.question.text = question;
        this.choice1.text = choice1;
        this.choice2.text = choice2;
        this.choice3.text = choice3;

        satisfaction[0] = sat1;
        satisfaction[1] = sat2;
        satisfaction[2] = sat3;
    }

    public void LastAnswer(string answer)
    {
        Question(
           answer,
           "",
           "",
           "",
           1, -1, 0);

        b_question.GetComponent<Button>().interactable = true;
        b_choice1.SetActive(false);
        b_choice2.SetActive(false);
        b_choice3.SetActive(false);
    }

    public void Question1()
    {
        player.SetPlayerDisabled(true);

        Question(
            "Why are you here?",
            "I wanted to clean the room",
            "It's none of your business",
            "I forgot my wallet",
            1, -1, 0);
    }

    void Question2()
    {
        LastAnswer("Great, here is the key");
    }

    void Question3()
    {
        LastAnswer("Fine, here you go");
    }

    void Question4()
    {
        LastAnswer("I don't trust you");
    }

    public void SelectAnswer(int choice)
    {
        switch (currentQuestion)
        {
            case 1:
                switch (satisfaction[choice - 1])
                {
                    case -1:
                        Question4();
                        break;
                    case 0:
                        Question3();
                        break;
                    case 1:
                        Question2();
                        break;
                }
                break;
            default:
                break;
        }
    }

    public void CloseDialogue()
    {
        //TODO give key if ok

        b_question.SetActive(false);
        player.SetPlayerDisabled(false);
        gameObject.SetActive(false);
    }
}
