using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnGameStateChanged;

    public enum GameState
    {
        WaitToStart = 0,
        Playing,
        GameOver
    }

    private GameState _state;
    private float _waitToStartTimer = 3f;
    private float _playingTimer = 10f;

    public float WaitToStartTimer { get => _waitToStartTimer; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _state = GameState.WaitToStart;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (_state)
        {
            case GameState.WaitToStart:
                _waitToStartTimer -= Time.deltaTime;

                if (_waitToStartTimer < 0)
                {
                    _state = GameState.Playing;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.Playing:
                _playingTimer -= Time.deltaTime;

                if (_playingTimer < 0)
                {
                    _state = GameState.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GameOver:
            default:
                break;
        }
    }

    public bool IsCountingDown()
    {
        return _state == GameState.WaitToStart;
    }

    public bool IsPlaying()
    {
        return _state == GameState.Playing;
    }
}
