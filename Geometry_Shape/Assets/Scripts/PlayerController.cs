using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Muerte
    public Transform checkPoint;

    //Estadísticas
    public int nJump;
    public int totalTime;

    //Cambio de forma
    public int lastShape;
    public int shape = 1;

    //Referencias
    public Rigidbody2D _rigidbody;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform[] children;
    public MecanicsRectangle _mecanicsRectangle;

    //Audio
    public SoundManager _soundManager;

    //Movimiento
    public Vector2 _movement;
    public float speed = 8f;
    public float horizontalVelocity;
    public float horizontalInput;
    public bool facingRight;

    //Salto
    public float jumpforce = 10f;
    public bool _isGrounded;
    public bool _isJumping = false;
    public float groundCheckRadius;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        children = GetComponentsInChildren<Transform>();
        groundCheck = children[2].GetComponent<Transform>();
        _mecanicsRectangle = GetComponentInChildren<MecanicsRectangle>();

        children[3].gameObject.SetActive(false);
        children[5].gameObject.SetActive(false);
        children[7].gameObject.SetActive(false);
    }

    void Update()
    {
        //Reinicia la escena actual
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Cambio de forma
        if (_isGrounded)
        {
            if (Input.GetKeyDown("1"))
            {
                lastShape = shape;
                shape = 1;

                //Cuando se cambia de forma desde "Círculo" a cualquier otra debe aumentar la fuerza de salto
                if (lastShape == 5)
                {
                    jumpforce += 5;
                }

                ShapeChange(1);
            }
            else if (Input.GetKeyDown("2"))
            {
                lastShape = shape;
                shape = 3;

                if (lastShape == 5)
                {
                    jumpforce += 5;
                }

                ShapeChange(2);
            }
            else if (Input.GetKeyDown("3"))
            {
                lastShape = shape;
                shape = 5;
                ShapeChange(3);

            }
            else if (Input.GetKeyDown("4"))
            {
                lastShape = shape;
                shape = 7;

                if (lastShape == 5)
                {
                    jumpforce += 5;
                }

                ShapeChange(4);
            }



        }

        totalTime = (int)Time.time;

        //¿Estoy saltando?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //Salto en FixedUpdate
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        //Movimiento
        if (shape == 5)
        {
            children[shape].GetComponent<MecanicsCircle>().Accelerate();
        }
        else
        {
            Move();
        }
        

        //Salto
        if (_isJumping && _isGrounded)
        {
            Jump(false);
        }
    }


    public void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(horizontalInput, 0f);
        horizontalVelocity = _movement.normalized.x * speed;

        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);

        if (horizontalInput == -1 && facingRight || horizontalInput == 1 && !facingRight) //El personaje gira para mirar hacia donde se mueve
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    public void Jump(bool dj)
    {
        _isJumping = false;
        if (dj)
        {
            _soundManager.Jump();
            _rigidbody.velocity = Vector2.up * 0;
            _rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
            nJump += 1;

            if(_rigidbody.velocity.y > 35)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 35);
            }
        }
        else
        {
            _soundManager.Jump();
            _rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
            nJump += 1;

            if (_rigidbody.velocity.y > 35)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 35);
            }
        }
    }


    public void ShapeChange(int select)
    {
        if (lastShape == 7 && _mecanicsRectangle.turned)
        {
            _mecanicsRectangle.Turn();
            _mecanicsRectangle.turned = false;
        }

        switch (select)
        {
            case 1:
                if (lastShape != 1)
                {
                    _soundManager.ShapeChange();
                    children[lastShape].gameObject.SetActive(false);
                    children[shape].gameObject.SetActive(true);
                    groundCheck = children[shape + 1].GetComponentInChildren<Transform>();
                }
                break;
                
            case 2:
                if (lastShape != 3)
                {
                    _soundManager.ShapeChange();
                    children[lastShape].gameObject.SetActive(false);
                    children[shape].gameObject.SetActive(true);
                    groundCheck = children[shape + 1].GetComponentInChildren<Transform>();
                }
                break;
            case 3:
                if (lastShape != 5)
                {
                    _soundManager.ShapeChange();
                    children[lastShape].gameObject.SetActive(false);
                    children[shape].gameObject.SetActive(true);
                    groundCheck = children[shape + 1].GetComponentInChildren<Transform>();
                    jumpforce -= 5; //Cuando se cambia a "Círculo" debe disminuir la fuerza de salto
                }
                break;
            case 4:
                if (lastShape != 7)
                {
                    _soundManager.ShapeChange();
                    children[lastShape].gameObject.SetActive(false);
                    children[shape].gameObject.SetActive(true);
                    groundCheck = children[shape + 1].GetComponentInChildren<Transform>();
                }
                break;
        }
    }

    public void Death()
    {
        children[shape].gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        StartCoroutine(WaitDeath());
    }

    private IEnumerator WaitDeath()
    {
        Time.timeScale = 0.01f;
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 1;

        transform.position = checkPoint.GetComponentInChildren<Transform>().position;
        children[shape].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        
        if (shape == 7 && GetComponentInChildren<MecanicsRectangle>().turned == true)
        {
            GetComponentInChildren<MecanicsRectangle>().Turn();
        }

        jumpforce = 35f;
        lastShape = shape;
        shape = 1;
        ShapeChange(1);
    }
}
