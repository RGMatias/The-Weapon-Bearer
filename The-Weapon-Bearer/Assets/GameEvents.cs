using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    // Delegado para manejar la colisi�n con el suelo
    public delegate void ColisionSuelo(Collision2D collision);

    // Evento que se activar� cuando el jugador toque el suelo
    public static event ColisionSuelo colisionSuelo;

    // M�todo para invocar el evento
    public static void ActivarColisionSuelo(Collision2D collision)
    {
        colisionSuelo?.Invoke(collision);
    }
}
