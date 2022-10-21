using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMenager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource startGameSound;
    [SerializeField] TextMeshProUGUI input;

    private void Awake()
    {
        LoadUsername();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        startGameSound.PlayOneShot(startGameSound.clip, 1);
        StartCoroutine(AsyncStartGame());
    }
    private IEnumerator AsyncStartGame()
    {
        animator.Play("StartTheGame");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    public void SaveUsername()
    {
        PlayerPrefs.SetString("username", input.text);
    } 
    public void LoadUsername()
    {
        string username = PlayerPrefs.GetString("username");
        input.text = username;
    }

}
