using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables públicas para el movimiento y el salto
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float forceJump = 5f;

    private Rigidbody2D rb;
    private bool puedeSaltar = false; // Bandera para controlar si se puede saltar

    // Evento para manejar la colisión con el suelo
    private void OnColisionSuelo(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            puedeSaltar = true;  // Permite saltar al tocar el suelo
        }
    }

    // Inicialización de componentes y suscripción al evento
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtener el componente Rigidbody2D
        GameEvents.colisionSuelo += OnColisionSuelo;  // Suscribimos al evento
    }

    // Llamado una vez por frame para manejar la entrada de usuario
    void Update()
    {
        movimientoHorizontal();  // Llamamos al método para mover horizontalmente

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

    // Método de limpieza: desuscribimos del evento cuando el objeto es destruido
    void OnDestroy()
    {
        GameEvents.colisionSuelo -= OnColisionSuelo;  // Desuscribimos al evento
    }

    // Detecta la colisión y activa el evento para informar que tocamos el suelo
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameEvents.ActivarColisionSuelo(collision);  // Activar el evento de colisión con el suelo
    }
}
