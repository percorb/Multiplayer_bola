using UnityEngine;

public class Rotacion : MonoBehaviour
{
    public float RotationSpeed = 100f;
    public float LevitationHeight = 0.3f;  // Altura máxima de levitación
    public float LevitationSpeed = 1f;  // Velocidad del movimiento de levitación

    //private 
    private Vector3 initialPosition; //Posición inicial
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotación
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);

        //Efecto levitacion
        float Offset = Mathf.Sin(2*Time.time * LevitationSpeed) * LevitationHeight;
        transform.position = new Vector3(initialPosition.x, initialPosition.y + Offset + 0.3f, initialPosition.z);
    }
}
