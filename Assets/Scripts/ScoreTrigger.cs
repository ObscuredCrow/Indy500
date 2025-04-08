using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player") return;
        if (!collider.GetComponent<CarController>().Player1) return;

        GameController.Instance.AddToLap(1); 
    }
}
