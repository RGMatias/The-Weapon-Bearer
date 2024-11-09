using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables p�blicas para el movimiento y el salto
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float forceJump = 5f;

    private Rigidbody2D rb;
    private bool puedeSaltar = false; // Bandera para controlar si se puede saltar

    // Evento para manejar la colisi�n con el suelo
    private void OnColisionSuelo(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            puedeSaltar = true;  // Permite saltar al tocar el suelo
        }
    }

    // Inicializaci�n de componentes y suscripci�n al evento
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtener el componente Rigidbody2D
        GameEvents.colisionSuelo += OnColisionSuelo;  // Suscribimos al evento
    }

    // Llamado una vez por frame para manejar la entrada de usuario
    void Update()
    {
        movimientoHorizontal();  // Llamamos al m�todo para mover horizontalmente

        // Si la tecla de salto (W) es presionada y el jugador puede saltar
        if (Input.GetKeyDown(KeyCode.W) && puedeSaltar)
        {
            Salto();  // Ejecutamos el salto
        }
    }

    // Movimiento horizontal del jugador
    private void movimientoHorizontal()
    {
        float horizontal = Input.GetAxis("Horizontal");  // Obtener el input horizontal
        Vector2 movement = new Vector2(horizontal, 0) * moveSpeed;  // Movimiento en X

        // Asignamos el movimiento horizontal, manteniendo la velocidad vertical
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
    }

    // Salto: modificar la velocidad en el eje Y
    private void Salto()
    {
        rb.velocity = new Vector2(rb.velocity.x, forceJump);  // Aplicar fuerza al salto en Y
        puedeSaltar = false;  // Evitar que el jugador salte nuevamente hasta tocar el suelo
    }

    // M�todo de limpieza: desuscribimos del evento cuando el objeto es destruido
    void OnDestroy()
    {
        GameEvents.colisionSuelo -= OnColisionSuelo;  // Desuscribimos al evento
    }

    // Detecta la colisi�n y activa el evento para informar que tocamos el suelo
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameEvents.ActivarColisionSuelo(collision);  // Activar el evento de colisi�n con el suelo
    }
}
