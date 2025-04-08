using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject _play;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private TMP_Text _lap;

    private void SetVisibility() {
        _start.SetActive(GameController.Instance.State == GameState.Start);
        _play.gameObject.SetActive(GameController.Instance.State == GameState.Run);
        _gameOver.SetActive(GameController.Instance.State == GameState.Stop);
    }

    public void ModeOnChanged(int index) => GameController.Instance.IsDuoPlay = index == 1;

    public void MapOnChanged(int index) {
        GameController.Instance.Course1.SetActive(index == 0);
        GameController.Instance.Course2.SetActive(index == 1);
    }

    public void SetLap(int lap) => _lap.text = $"Lap: {lap}";

    private void Awake() {
        Instance ??= this;
    }

    private void Update() => SetVisibility();

}