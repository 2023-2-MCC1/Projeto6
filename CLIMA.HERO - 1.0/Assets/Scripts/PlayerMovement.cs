using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private int Thermometer = 0;
    [SerializeField] private Text ThermometerText;
    [SerializeField] private int thermometerGoal = 10;

    private float elapsedTime = 0f;
    private float maxElapsedTime = 180f;

    [SerializeField] private Text TimeText; // Renomeado para TimeText

    private enum MovementState { Idle, Running, Jumping, Falling, MidAir }

    [SerializeField] private AudioSource jumpSoundEffect;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Não é necessário chamar GetComponent<Text>() aqui se você já atribuiu a referência no Inspector.
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

        if (Thermometer >= thermometerGoal)
        {
            SceneManager.LoadScene("Final");
        }

        elapsedTime += Time.deltaTime;

        // Verifique se TimeText não é nulo antes de tentar acessá-lo
        if (TimeText != null)
        {
            // Atualiza o texto do objeto de texto para mostrar o tempo decorrido
            TimeText.text = "Time: " + Mathf.Floor(elapsedTime).ToString();
        }

        if (elapsedTime >= maxElapsedTime)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.Running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.Running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.Idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.Jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.Falling;
        }

        anim.SetInteger("State", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private AudioSource winSoundEffect;
    [SerializeField] private AudioSource enemySoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Thermometer"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            Thermometer++;
            ThermometerText.text = "" + Thermometer;
            if (Thermometer >= thermometerGoal)
        {
            winSoundEffect.Play();
        }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<EnemyOne>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collision.gameObject, 1f);
            enemySoundEffect.Play();
        }
    }
}
