using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenager : MonoBehaviour
{
    public Animator animator;
    public Canvas pauseMenu;
    public Canvas buildMenu;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.enabled)
            {
                FadeIn();
                pauseMenu.enabled = true;
            }
            else
            {
                FadeOut();
                pauseMenu.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (buildMenu.enabled)
                buildMenu.enabled = false;
            else
                buildMenu.enabled = true;
        }
    }

    public void FadeIn()
    {
        animator.Play("FadeIn");
    }
    public void FadeOut()
    {
        animator.Play("FadeOut");
    }

    public void StartRestartScene()
    {
        StartCoroutine(AsyncRestartScene());
    }

    private IEnumerator AsyncRestartScene()
    {
        FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
