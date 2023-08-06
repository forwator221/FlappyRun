using UnityEngine;

public class Coin : MonoBehaviour
{  
    [SerializeField] private float _collectingDistance;
    [SerializeField] private float _speed;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        var distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance < _collectingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _player.AddCoin();
            Destroy(gameObject);
        }
    }
}
