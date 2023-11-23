using UnityEngine;
using UnityEngine.SceneManagement;

// Este script representa um objeto que, ao colidir com uma "linha invisível" identificada pela tag "InvisibleLine",
// executa uma animação de morte, desativa a física do objeto, e reinicia o nível após um atraso.

public class InvisibleLine : MonoBehaviour
{
    // Referências para os componentes Animator e Rigidbody2D.
    private Animator anim;
    private Rigidbody2D rb;

    // Método chamado no início do script.
    private void Start()
    {
        // Inicializa os componentes Animator e Rigidbody2D.
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Método chamado quando outro collider entra no trigger deste objeto.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o collider que entrou é identificado pela tag "InvisibleLine" e executa a lógica de morte.
        if (other.CompareTag("InvisibleLine"))
        {
            Die();
        }
    }

    // Lógica de morte do objeto.
    private void Die()
    {
        // Desativa a física do objeto, reproduz a animação de morte e inicia a corotina para reiniciar o nível após um atraso.
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
        StartCoroutine(RestartLevelAfterDelay(1.0f));
    }

    // Corotina para reiniciar o nível após um atraso.
    private System.Collections.IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartLevel();
    }

    // Método para reiniciar o nível.
    private void RestartLevel()
    {
        // Carrega a cena atual para reiniciar o nível.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
