using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script é responsável por aplicar um efeito de knockback ao jogador quando colide com objetos do tipo "Enemy" ou "Trap".

public class TriggerDamage : MonoBehaviour
{
    // Força do knockback aplicada ao jogador.
    public float knockbackForce = 5f;

    // Duração do knockback, controla por quanto tempo o jogador perde o controle.
    public float knockbackDuration = 0.5f;

    // Método chamado quando ocorre uma colisão.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a colisão ocorreu com um objeto identificado como "Enemy" ou "Trap".
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Trap"))
        {
            // Chame o método para aplicar o knockback.
            Knockback(collision.transform);
        }
    }

    // Método para aplicar o knockback ao jogador.
    private void Knockback(Transform enemy)
    {
        // Calcule a direção do knockback.
        Vector2 knockbackDirection = (transform.position - enemy.position).normalized;

        // Aplique a força do knockback usando um impulso.
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        // Desative temporariamente o controle do jogador durante o knockback.
        StartCoroutine(DisablePlayerControl());
    }

    // Corotina para desativar temporariamente o controle do jogador durante o knockback.
    IEnumerator DisablePlayerControl()
    {
        // Obtém a referência para o componente PlayerMovement.
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();

        // Verifica se o componente PlayerMovement foi encontrado.
        if (playerMovement != null)
        {
            // Desativa o componente de controle do jogador.
            playerMovement.enabled = false;

            // Aguarda a duração do knockback.
            yield return new WaitForSeconds(knockbackDuration);

            // Reativa o componente de controle do jogador.
            playerMovement.enabled = true;
        }
    }
}
