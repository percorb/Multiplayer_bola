using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Botones : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip rick;
    private bool playing=false;
    public Animator fade;
    public GameObject panel;
    public void Jugar(){
        panel.SetActive(true);
        fade.Play("Fade_in");
        StartCoroutine("Esperar");
    }

    private IEnumerator Esperar(){
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("main");
    }

    public void Salir(){
        Application.Quit();
    }

    public void Rick(){

        if(playing){
            audioSource.Stop();
            playing=false;
        }else{
            audioSource.PlayOneShot(rick);
            playing=true;
        }
    }
}
