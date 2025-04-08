using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Player 1")]
    [SerializeField] private GameObject _player1Car;
    [SerializeField] private Transform _player1CarSpawn;

    [Header("Player 2")]
    [SerializeField] private GameObject _player2Car;
    [SerializeField] private Transform _player2CarSpawn;

    [Header("AI")]
    [SerializeField] private GameObject _playerAICar;
    [SerializeField] private Transform _playerAICarSpawn;

    [Header("Course")]
    public GameObject Course1;
    public GameObject Course2;
    public Waypoints Course1Waypoints;
    public Waypoints Course2Waypoints;

    [HideInInspector] public GameState State = GameState.Start;
    [HideInInspector] public bool IsDuoPlay;

    private int _lap = 0;
    private AudioSource _audio;

    private void Update() {
        StartGame();
        ResetGame();
    }

    public void StartGame() {
        if (Input.GetKeyDown(KeyCode.Space) && State == GameState.Start) {
            State = GameState.Run;
            _lap = 0;
            AddToLap(0);

            if (IsDuoPlay) {
                Instantiate(_player1Car, _player1CarSpawn.position, Quaternion.identity);
                Instantiate(_player2Car, _player2CarSpawn.position, Quaternion.identity);
            }
            else {
                Instantiate(_player1Car, _player1CarSpawn.position, Quaternion.identity);
                Instantiate(_playerAICar, _playerAICarSpawn.position, Quaternion.identity);
            }
        }
    }

    public void AddToLap(int amount) {
        if (State != GameState.Run) return;

        _lap += amount;
        UIController.Instance.SetLap(_lap);

        if (_lap > 3)
            StopGame();
    }

    public void StopGame() { 
        State = GameState.Stop;
        foreach (var car in FindObjectsByType<CarController>(FindObjectsSortMode.None))
            Destroy(car.gameObject);
    }

    public void ResetGame() {
        if (Input.GetKeyDown(KeyCode.R) && State == GameState.Stop) {
            State = GameState.Start;
        }
    }

    private void Awake() { 
        Instance ??= this;
        _audio = GetComponent<AudioSource>();
    }
}