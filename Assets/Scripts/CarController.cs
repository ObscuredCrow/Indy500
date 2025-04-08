using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float _moveModifier = 0.5f;
    [SerializeField] private float _moveMax = 0.5f;
    [SerializeField] private float _turnModifier = 0.5f;
    [SerializeField] private float _frictionModifier = 0.5f;

    public bool Player1 = true;
    public bool PlayerAI = false;

    private Vector2 _currentMove;
    private Rigidbody2D _rigidbody;
    private AudioSource _audio;
    private KeyCode _forward = KeyCode.W;
    private KeyCode _backward = KeyCode.S;
    private KeyCode _left = KeyCode.A;
    private KeyCode _right = KeyCode.D;
    private int _currentCourse = 0;

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (GameController.Instance.State != GameState.Run) return;

        if (PlayerAI) {
            var point = _currentCourse == 0 ? GameController.Instance.Course1Waypoints.GetCurrentPoint() : GameController.Instance.Course2Waypoints.GetCurrentPoint();
            if (Vector2.Distance(transform.position, point.position) < 0.25f)
                point = _currentCourse == 0 ? GameController.Instance.Course1Waypoints.NextPoint() : GameController.Instance.Course2Waypoints.NextPoint();

            var newSpeed = UnityEngine.Random.Range(0.5f, 1.5f);
            transform.position = Vector2.MoveTowards(transform.position, point.position, newSpeed * Time.deltaTime);
            transform.LookAt2D(point.position);
        }
        else {
            Vector2 vertical = Input.GetKey(_forward) ? transform.up : Input.GetKey(_backward) ? -transform.up : Vector2.zero;
            _currentMove = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y);

            if (_currentMove.magnitude > _moveMax) {
                _currentMove = _currentMove.normalized;
                _currentMove *= _moveMax;
            }

            if (vertical != Vector2.zero) {
                _rigidbody.AddForce(vertical * _moveModifier);
                _rigidbody.linearDamping = _frictionModifier;
                transform.Rotate(Vector3.forward * (Input.GetKey(_left) ? _turnModifier : Input.GetKey(_right) ? -_turnModifier : 0));
            }
            else _rigidbody.linearDamping = _frictionModifier * 2;
        }
    }

    private void Awake() { 
        _audio = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (!Player1) {
            _forward = KeyCode.UpArrow;
            _backward = KeyCode.DownArrow;
            _left = KeyCode.LeftArrow;
            _right = KeyCode.RightArrow;
        }

        if (PlayerAI)
            _currentCourse = GameController.Instance.Course1.activeSelf ? 0 : 1;
    }
}