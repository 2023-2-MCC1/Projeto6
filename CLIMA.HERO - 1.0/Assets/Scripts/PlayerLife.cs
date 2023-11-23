using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Este script gerencia a vida do jogador, respondendo a colisões com uma "linha invisível" identificada pela tag "InvisibleLine".
// Quando o jogador colide com a linha invisível, ele entra no estado de morte, desativando sua física e reproduzindo a animação de morte.

public class PlayerLife : MonoBehaviour
{
    // Referências para os componentes Rigidbody2D e Animator do jogador.
    private Rigidbody2D rb;
    private Animator anim;

    // Método chamado no início do script.
    [SerializeField] private AudioSource deathSoundEffect;
    private void Start()
    {
        // Inicializa as referências aos componentes Rigidbody2D e Animator.
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Método chamado quando ocorre uma colisão.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a colisão ocorreu com um objeto identificado como "InvisibleLine".
        if (collision.gameObject.CompareTag("InvisibleLine"))
        {
            // Chama o método para tratar a morte do jogador.
            Die();
        }
    }

    // Método para tratar a morte do jogador.
    private void Die()
    {
        deathSoundEffect.Play();
        // Desativa a física do jogador.
        rb.bodyType = RigidbodyType2D.Static;

        // Reproduz a animação de morte.
        anim.SetTrigger("Death");

        // Método para reiniciar o nível (não está implementado no código fornecido).
        // Certifique-se de implementar este método de acordo com a lógica desejada.
        //RestartLevel();
    }

    // Método para reiniciar o nível (não está implementado no código fornecido).
    // Certifique-se de implementar este método de acordo com a lógica desejada.
    private void RestartLevel()
    {
        // Carrega a cena "GameOver" para reiniciar o nível.
        SceneManager.LoadScene("GameOver");
    }
}
