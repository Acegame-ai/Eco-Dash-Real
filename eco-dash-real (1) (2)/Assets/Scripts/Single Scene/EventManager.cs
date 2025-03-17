using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    // Unity Event for game state changes
    public UnityEvent<GameState> onGameStateChange = new UnityEvent<GameState>();

    // Unity Events for score and coin changes
    public UnityEvent<int> onScoreChanged = new UnityEvent<int>();
    public UnityEvent<int> onCoinsChanged = new UnityEvent<int>();

    // Unity Event for game over with final score and coins as parameters
    public UnityEvent<int, int> onGameOver = new UnityEvent<int, int>();
    public UnityEvent onGameRestart = new UnityEvent();
    public UnityEvent onPauseGame = new UnityEvent();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

