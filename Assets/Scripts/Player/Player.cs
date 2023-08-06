using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IMovable
{
    [Header("Player Params")]
    [SerializeField] private float _velocity = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;

    [SerializeField] private GameObject _uiText;

    private Rigidbody2D _playerRB;

    public int Coins { get; private set; }

    public bool _isStarted;
    private bool _isJumping;


    private void Start()
    {
        Time.timeScale = 0;
        _playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_isStarted)
        {
            _uiText.SetActive(true);
            StartGame();
        }
        if (_isStarted)
        {
            CheckJump();
        }
    }
    private void FixedUpdate()
    {
        if (_isStarted)
        {
            Move();
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        Respawn();
    }

    public void Move()
    {
        _playerRB.velocity = new Vector2(_velocity, _playerRB.velocity.y);
    }

    public void Respawn()
    {
        _isStarted = false;
        SceneManager.LoadScene(0);
    }

    public void AddCoin()
    {
        Coins++;
    }

    private void Jump()
    {
        if (_isJumping)
        {
            _playerRB.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        }

        if (_playerRB.velocity.y < 0)
        {
            _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (_playerRB.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _isJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            _isJumping = false;
        }
    }

    private void StartGame()
    {
        if (!_isStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                _isStarted = true;
                _uiText.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
