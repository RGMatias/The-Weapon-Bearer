using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitaEspada : MonoBehaviour
{
    public Transform centro; 
    public float radio = 3f; 
    public float velocidadAngular = 50f; 

    private float angulo; 

    void Update()
    {
        if (centro != null)
        {
            if (Input.GetMouseButton(0)) 
            {
                angulo += -velocidadAngular * Time.deltaTime;
            }
            else
            {
                angulo += 0 * Time.deltaTime;
            }

            if (Input.GetMouseButton(1)) 
            {
                angulo += velocidadAngular * Time.deltaTime;
            }
            else
            {
                angulo += 0 * Time.deltaTime;
            }
            float anguloRad = angulo * Mathf.Deg2Rad;

            Vector2 nuevaPosicion = new Vector2(
                centro.position.x + Mathf.Cos(anguloRad) * radio, // X
                centro.position.y + Mathf.Sin(anguloRad) * radio  // Y
            );

            transform.position = nuevaPosicion;

            Vector2 direccionHaciaCentro = (Vector2)centro.position - nuevaPosicion;
            float anguloRotacion = Mathf.Atan2(direccionHaciaCentro.y, direccionHaciaCentro.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, anguloRotacion);
        }
    }
}
