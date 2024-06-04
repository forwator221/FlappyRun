using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IMovable
{
    private const string LEADER_BOARD_NAME = "CoinsScore";

    [Header("Player Params")]
    [SerializeField] private float _velocity = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;

    [SerializeField] private GameObject _uiText;
    [SerializeField] private GameObject _buttonsPanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _coinShop;
    [SerializeField] private Button _continueShop;
    [SerializeField] private int _continueCounter = 3;
    [SerializeField] private LangYGAdditionalText _continueCounterText;

    private Rigidbody2D _playerRB;

    public int Coins { get; private set; }

    public bool _isStarted;
    private bool _isJumping;

    private int _maxCoins;
    private string _attemtsText;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += AddReward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= AddReward;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _buttonsPanel.SetActive(false);
        _playerRB = GetComponent<Rigidbody2D>();
        _maxCoins = YandexGame.savesData.MaxCoins;
        _continueCounterText.additionalText = _continueCounter.ToString();
        _continueShop.onClick.AddListener(delegate{ShowCoinAdd(1);});
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
        Time.timeScale = 0;
        _buttonsPanel.SetActive(true);
    }

    public void Move()
    {
        _playerRB.velocity = new Vector2(_velocity, _playerRB.velocity.y);
    }

    public void Die()
    {
        Time.timeScale = 0;
        _buttonsPanel.SetActive(true);
    }

    public void Respawn()
    {
        _isStarted = false;
        _buttonsPanel.SetActive(false);

        if (YandexGame.savesData.MaxCoins < Coins)
        {
            YandexGame.savesData.MaxCoins = Coins;
            YandexGame.SaveProgress();
            YandexGame.NewLeaderboardScores(LEADER_BOARD_NAME, Coins);
        }

        _continueCounter = 3;
        Coins = 0;

        SceneManager.LoadScene(0);
    }

    public void AddCoin()
    {
        Coins++;
    }

    public void AddReward(int id)
    {
        Time.timeScale = 0;
        _isStarted = false;
        _buttonsPanel.SetActive(false);
        _continueCounter--;
        _continueCounterText.additionalText = _continueCounter.ToString();

        if (_continueCounter < 1)
            _continueShop.gameObject.SetActive(false);

        _coinShop.gameObject.SetActive(false);
    }

    public void ShowCoinAdd(int id)
    {
        YandexGame.RewVideoShow(id);
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
