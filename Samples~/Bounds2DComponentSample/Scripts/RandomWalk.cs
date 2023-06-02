#region

using UnityEngine;

#endregion

namespace Klem.Samples.Bounds2DComponentSample.Scripts
{
    public class RandomWalk : MonoBehaviour
    {
        private float _currentSpeed;
        private Vector2 _currentDirection;
        private float _currentTimer;
        private float _currentInterval;
        private Vector2 _currentVelocity;

        [SerializeField] private float minSpeed = 0.5f;
        [SerializeField] private float maxSpeed = 1.5f;
        [SerializeField] private float minInterval = 1f;
        [SerializeField] private float maxInterval = 3f;

        private void Start()
        {
            _currentInterval = Random.Range(minInterval, maxInterval);
            _currentSpeed = Random.Range(minSpeed, maxSpeed);
            _currentDirection = Random.insideUnitCircle.normalized;
        }

        private void Update()
        {
            _currentTimer += Time.deltaTime;
            if (_currentTimer >= _currentInterval)
            {
                _currentDirection = Random.insideUnitCircle.normalized;
                _currentSpeed = Random.Range(minSpeed, maxSpeed);
                _currentTimer = 0f;
                _currentInterval = Random.Range(minInterval, maxInterval);
            }

            _currentVelocity = _currentDirection * _currentSpeed;
            transform.position += (Vector3)_currentVelocity * Time.deltaTime;
            transform.localRotation = Quaternion.LookRotation(Vector3.forward, _currentVelocity);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var position = transform.position;
            Gizmos.DrawLine(position, position + (Vector3)_currentVelocity);
        }
    }
}