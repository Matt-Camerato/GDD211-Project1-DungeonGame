using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAgain() => anim.SetTrigger("playAgain");
    public void MainMenu() => anim.SetTrigger("mainMenu");

    
    private void RestartGame() => SceneManager.LoadScene(1); //anaimation event called when game fades out for play again
    private void QuitGame() => SceneManager.LoadScene(0); //animation event called when game fades out to main menu
}
