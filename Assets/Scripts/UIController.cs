using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

    public static UIController instance;
    public GameObject ScrollUI;
    public GameObject CancelBtn;
    public GameObject TutorialBlock;
    public GameObject Blocker;
    public GameObject Blocker1;
    public Text tutorialText;
    public GameObject Up, Down;

    void Awake()
    {
        //PlayerPrefs.DeleteAll();      //Tutorial for every Launch
        if(!PlayerPrefs.HasKey ("FirstTime"))
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            Blocker.SetActive(true);
            Down.SetActive(true);
            tutorialText.text = "Click the type of building you want to generate \n and move the mouse to position the object.";
        }
        instance = this;
    }

    public void Clicked_3x3 () 
    {
        MainController.instance.SpawnBuilding("3x3");
        DisableScrollUI();
    }

    public void Clicked_3x5() 
    {
        MainController.instance.SpawnBuilding("3x5");
        DisableScrollUI();
    }

    public void Clicked_5x5()
    {
        MainController.instance.SpawnBuilding("5x5");
        DisableScrollUI();
    }

    public void Clicked_7x7()
    {
        MainController.instance.SpawnBuilding("7x7");
        DisableScrollUI();
    }

    public void Clicked_7x10()
    {
        MainController.instance.SpawnBuilding("7x10");
        DisableScrollUI();
    }

    public void Clicked_10x5()
    {
        MainController.instance.SpawnBuilding("10x5");
        DisableScrollUI();
    }

    public void Clicked_10x10()
    {
        MainController.instance.SpawnBuilding("10x10");
        DisableScrollUI();
    }

    public void Clicked_10x15()
    {
        MainController.instance.SpawnBuilding("10x15");
        DisableScrollUI();
    }

    public void Clicked_12x13()
    {
        MainController.instance.SpawnBuilding("12x13");
        DisableScrollUI();
    }

    public void Clicked_15x7()
    {
        MainController.instance.SpawnBuilding("15x7");
        DisableScrollUI();
    }

    public void Clicked_15x15()
    {
        MainController.instance.SpawnBuilding("15x15");
        DisableScrollUI();
    }

    public void EnableScrollUI()
    {
        MainController.instance.DestroyCurrentBuilding();
        ScrollUI.SetActive(true);
        CancelBtn.SetActive(false);
    }

    public void DisableScrollUI()
    {
        if(PlayerPrefs.GetInt("FirstTime") == 1)
        {
            Blocker.SetActive(false);
            Blocker1.SetActive(true);
            Down.SetActive(false);
            Up.SetActive(true);
            tutorialText.text = "Press left mouse button to place the building \ninside the grass.";
            StartCoroutine("TurnOffTutorial");
        }
        ScrollUI.SetActive(false);
        CancelBtn.SetActive(true);
    }

    private IEnumerator TurnOffTutorial ()
    {
        yield return new WaitForSeconds(3);
        Up.SetActive(false);
        tutorialText.text = string.Empty;
        Blocker1.SetActive(false);
        TutorialBlock.SetActive(false);
    }
}
