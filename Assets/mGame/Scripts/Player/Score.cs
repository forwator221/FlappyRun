using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _scoreText;

    private void Update()
    {
        _scoreText.text = ((int)(_player.Coins)).ToString();
    }
}
