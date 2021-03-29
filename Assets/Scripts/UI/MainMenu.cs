using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //triggers "start game" animation
    public void PlayButton()
    {
        anim.SetTrigger("startGame");
    }

    //this is animation event that gets triggered after "start game" animation plays
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //toggle whether how-to-play panel is open or not
    public void ToggleInstructions()
    {
        anim.SetBool("instructions", !anim.GetBool("instructions"));
    }
}
