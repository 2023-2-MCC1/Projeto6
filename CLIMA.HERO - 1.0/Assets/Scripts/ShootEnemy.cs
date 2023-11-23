using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script controla o comportamento de um inimigo que dispara projéteis em intervalos regulares.

public class ShootEnemy : MonoBehaviour
{
    // Referência para o prefab do projétil.
    [SerializeField] private Rigidbody2D shoot;

    // Velocidade do projétil.
    [SerializeField] private float speedShoot;

    // Posição de onde os projéteis serão disparados.
    [SerializeField] private Transform positionShoot;

    // Tempo entre cada disparo.
    [SerializeField] private float timeBetween = 2f;

    // Método chamado a cada quadro.
    void Update()
    {
        // Verifica se é hora de disparar novamente.
        if (timeBetween <= 0)
        {
            // Chama o método para disparar e redefine o tempo entre disparos.
            Shoot();
            timeBetween = 2f;
        }
        else
        {
            // Decrementa o tempo entre disparos.
            timeBetween -= Time.deltaTime;
        }
    }

    // Método para realizar o disparo.
    void Shoot()
    {
        // Instancia um novo projétil na posição de disparo.
        Rigidbody2D clone = Instantiate(shoot, positionShoot.transform.position, positionShoot.transform.rotation);

        // Define a velocidade do projétil.
        clone.velocity = transform.right * speedShoot;

        // Destroi o projétil após um determinado tempo.
        Destroy(clone.gameObject, 1.7f);
    }

    // Método chamado quando ocorre uma colisão.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o inimigo colidiu com o chão e o destrói.
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
