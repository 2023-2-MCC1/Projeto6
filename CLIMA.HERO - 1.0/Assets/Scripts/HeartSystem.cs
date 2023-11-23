using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Este script gerencia o sistema de vidas do jogador, controlando as vidas, a lógica de morte,
// a atualização visual das vidas na interface do usuário e a resposta a colisões com armadilhas e inimigos.

public class HeartSystem : MonoBehaviour
{
    // Referências para componentes importantes do jogador.
    PlayerMovement player;
    Animator anim;
    Rigidbody2D rb;

    // Indica se o jogador está morto.
    public bool Death;

    // Número atual de vidas do jogador e vida máxima permitida.
    public int vida;
    public int vidaMaxima;

    // Array de imagens que representam as vidas na interface do usuário.
    public Image[] coracao;

    // Sprites para coração cheio e vazio.
    public Sprite cheio;
    public Sprite vazio;

    // Método chamado no início do script.
    void Start()
    {
        // Inicializa as referências aos componentes do jogador.
        player = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Inicializa o número de vidas do jogador com o valor máximo.
        vida = vidaMaxima;
    }

    // Método chamado a cada quadro.
    void Update()
    {
        // Atualiza a lógica de vida e verifica o estado de morte.
        AtualizarLogicaVida();
        EstadoMorto();
    }

    // Atualiza a lógica das vidas e a representação visual na interface do usuário.
    void AtualizarLogicaVida()
    {
        // Garante que o número de vidas não ultrapasse o máximo permitido.
        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        // Atualiza a representação visual das vidas na interface do usuário.
        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
            {
                coracao[i].sprite = cheio;
            }
            else
            {
                coracao[i].sprite = vazio;
            }

            // Ativa ou desativa a imagem do coração com base no número máximo de vidas permitido.
            if (i < vidaMaxima)
            {
                coracao[i].enabled = true;
            }
            else
            {
                coracao[i].enabled = false;
            }
        }
    }

    // Verifica se o jogador está morto e executa as ações correspondentes.

    [SerializeField] private AudioSource danoSoundEffect;
    void EstadoMorto()
    {
        if (vida <= 0 && !Death)
        {
            // Marca o jogador como morto, reproduz a animação de morte,
            // desabilita o controle do jogador e aguarda antes de reiniciar o nível.
            Death = true;
            anim.SetTrigger("Death");
            player.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            StartCoroutine(RestartarNivelAposDelay(2.0f)); // Reinicia o nível após 2 segundos
        }
    }

    // Método chamado quando ocorre uma colisão.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a colisão ocorreu com uma armadilha ou inimigo e executa a lógica de morte.
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            danoSoundEffect.Play();
            Morrer();
        }
    }

    // Lógica de morte do jogador.
    private void Morrer()
    {
        if (!Death && vida > 0)
        {
            // Marca o jogador como morto, decrementa o número de vidas
            // e executa ações correspondentes com base nas vidas restantes.
            Death = true;
            vida--;

            if (vida <= 0)
            {
                // Se não houverem vidas restantes, reproduz a animação de morte,
                // desabilita o controle do jogador e aguarda antes de reiniciar o nível.
                anim.SetTrigger("Death");
                player.enabled = false;
                rb.bodyType = RigidbodyType2D.Static;
                StartCoroutine(RestartarNivelAposDelay(2.0f)); // Reinicia o nível após 2 segundos
            }
            else
            {
                // Se ainda houverem vidas restantes, reseta o estado do jogador após um atraso.
                StartCoroutine(ResetarJogadorAposDelay(2.0f)); // Reseta o jogador após 2 segundos
            }
        }
    }

    // Corotina para resetar o estado do jogador após um atraso.
    IEnumerator ResetarJogadorAposDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reseta o estado do jogador.
        player.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;

        Death = false;
    }

    // Corotina para reiniciar o nível após um atraso.
    IEnumerator RestartarNivelAposDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReiniciarNivel();
    }

    // Método para reiniciar o nível.
    void ReiniciarNivel()
    {
        // Carrega a cena "GameOver" para reiniciar o nível.
        SceneManager.LoadScene("GameOver");
    }
}
