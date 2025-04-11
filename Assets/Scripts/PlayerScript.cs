using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;

namespace HelloWorld{

    public class PlayerScript : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        public float velocidad;
        public Rigidbody rb;
        public AudioSource audioSource;
        public AudioClip Collect_verde;
        public AudioClip Collect_azul;
        public TMP_Text texto;
        private int puntos=0;
        private bool estaEnSuelo = false;
        private Vector3 spawn;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            spawn = transform.position;
        }
        // Detectar si estamos tocando el suelo
        void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Suelo"))
            {
                estaEnSuelo = true;
            }
        }

        // Detectar si dejamos de tocar el suelo
        void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Suelo"))
            {
                estaEnSuelo = false;
            }
        }

        void ActualizarTexto(){
            texto.text= "Puntos: " + puntos + "/10";
        }
        void OnTriggerEnter(Collider other){
            if(other.gameObject.CompareTag("Rupia_Verde")){
                audioSource.PlayOneShot(Collect_verde);
                other.gameObject.SetActive(false); //Se desactiva el objeto
                puntos++;
                ActualizarTexto();

                if(puntos>=10){
                    StartCoroutine("Final");
                }
            }
            else if(other.gameObject.CompareTag("Rupia_Azul")){
                audioSource.PlayOneShot(Collect_azul);
                other.gameObject.SetActive(false); //Se desactiva el objeto
                puntos+=2;
                ActualizarTexto();

                if(puntos>=10){
                    StartCoroutine("Final");
                }
            }
        }

        
        // Update is called once per frame
        void Update()
        {
            if(rb.transform.position.y<=-3){
                StartCoroutine("respawn");
            }   
            if(IsOwner){
                float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;
                float vertical = Input.GetAxis("Vertical") * Time.deltaTime * velocidad;
                rb.transform.position += new Vector3(horizontal, 0, vertical); // Mueve la bola
                Position.Value = rb.transform.position; // Actualiza la posición del jugador en la red
                if(Input.GetKeyDown(KeyCode.Space)){
                    Salto();
                }
            }
            if(IsServer){
                Position.Value = rb.transform.position; // Actualiza la posición del jugador en la red
            }
        }

        private IEnumerator respawn(){
            yield return new WaitForSeconds(1); // Espera mientras se reproduce el fade in
            transform.position = spawn; // Respawnea la bola en su posición original
        }

        private IEnumerator Final(){
            yield return new WaitForSeconds(2); // Espera mientras se reproduce el fade in
            if(this.gameObject.CompareTag("Player_2")){
                SceneManager.LoadScene("Fin");
            }
            else
                SceneManager.LoadScene("Dos");
        }

        public void Salto(){
            if(estaEnSuelo){
                rb.AddForce(new Vector3(0,60,0),ForceMode.Impulse);
            }
        }
    }
}
