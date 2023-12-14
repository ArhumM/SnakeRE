using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody2D rb;

    private List<Transform> _snakeSpawn;
    public Transform snakePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(moveSpeed, 0);

        _snakeSpawn = new List<Transform>();
        _snakeSpawn.Add(this.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(0, moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = new Vector2(0, -moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
    }

    private void FixedUpdate()
    {
        for (int i = _snakeSpawn.Count - 1; i>0; i--)
        {
            _snakeSpawn[i].position = _snakeSpawn[i-1].position;
        }
    }

    private void grow()
    {
        Transform snakeSegment = Instantiate(snakePrefab);
        snakeSegment.position = _snakeSpawn[_snakeSpawn.Count - 1].position;

        _snakeSpawn.Add(snakeSegment);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            grow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}