using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float velocidade = 5f;
    public LayerMask layerBloco;

    private bool viradoParaDireita = true;

    void Update()
    {
        VerificarBlocoAbaixo();

        // Movimento horizontal
        float movimentoHorizontal = viradoParaDireita ? velocidade : -velocidade;
        transform.Translate(new Vector2(movimentoHorizontal * Time.deltaTime, 0));
    }

    void VerificarBlocoAbaixo()
    {
        // Raio para verificar a presença de um bloco abaixo do inimigo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, layerBloco);

        if (hit.collider == null)
        {
            TrocarDirecao();
        }
    }

    void TrocarDirecao()
    {
        // Trocar de posição indo para o outro lado e inverter a orientação
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        viradoParaDireita = !viradoParaDireita;
    }
}
