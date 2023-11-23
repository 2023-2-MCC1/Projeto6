using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script controla o movimento da câmera, mantendo-a alinhada com a posição do jogador.

public class CameraController : MonoBehaviour
{
    // Referência para o objeto Transform do jogador.
    [SerializeField] private Transform player;

    // O método Update é chamado a cada quadro.
    private void Update()
    {
        // Atualiza a posição da câmera para coincidir com a posição atual do jogador,
        // mantendo a mesma coordenada z da câmera para preservar a perspectiva.
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}

