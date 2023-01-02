using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject MenuScreen;
    public GameObject TitleBox;
    bool MenuToggle = true;
    void Start()
    {
        
    }
    public void EnterMenu() {
        MenuScreen.SetActive(MenuToggle);
        TitleBox.SetActive(false);
        

    }
}
