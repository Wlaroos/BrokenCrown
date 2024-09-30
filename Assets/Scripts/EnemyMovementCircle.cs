using System.Collections;
using UnityEngine;

public class EnemyMovementCircle : MonoBehaviour
{
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _force = 5f;
    [SerializeField] private GameObject _warningPrefab;
    [SerializeField] private GameObject _itemPrefab; // Prefab for the item to throw
    [SerializeField] private float _startDelay = 1f;
    [SerializeField] private float _circleRadius = 3f; // Radius for circling around the player
    [SerializeField] private float _chaseDistance = 5f; // Distance at which the enemy starts circling
    [SerializeField] private float _minThrowDistance = 5f; // Minimum distance to throw an item

    private Transform _playerRef;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private EnemyHealth _eh;

    private const float ForcePower = 10f;
    private Vector2 direction;
    private bool _canMove = false;
    private float _angle;

    private void Awake()
    {
        _playerRef = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _eh = GetComponent<EnemyHealth>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _playerRef.position - transform.position, 100f, LayerMask.GetMask("RaycastLayer"));
        Instantiate(_warningPrefab, hit.point, Quaternion.identity);
    }

    private void Start()
    {
        StartCoroutine(StartDelay());
        StartCoroutine(ThrowItemCoroutine());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null && !_eh.IsDowned)
        {
            var directionTowardsTarget = (_playerRef.position - this.transform.position).normalized;
            other.GetComponent<PlayerHealth>().TakeDamage(directionTowardsTarget, 1);
        }
    }

    private void Update() 
    {
        if (!_eh.IsDowned && _canMove)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _playerRef.position);

            if (distanceToPlayer < _chaseDistance)
            {
                // Calculate the angle for circling
                _angle += Time.deltaTime; // Adjust this to control the speed of circling
                float x = _playerRef.position.x + Mathf.Cos(_angle) * _circleRadius;
                float y = _playerRef.position.y + Mathf.Sin(_angle) * _circleRadius;
                Vector2 circlePosition = new Vector2(x, y);
                direction = (circlePosition - (Vector2)transform.position).normalized;
            }
            else
            {
                // Chase the player
                var directionTowardsTarget = (_playerRef.position - this.transform.position).normalized;
                direction = directionTowardsTarget;
            }

            // Make enemy look at the direction of movement
            _sr.flipX = direction.x < 0;

            MoveTo(direction);
        }
    }

    private void FixedUpdate() 
    {
        if (!_eh.IsDowned && _canMove)
        {
            // Movement
            var desiredVelocity = direction * _speed;
            var deltaVelocity = desiredVelocity - _rb.velocity;
            Vector3 moveForce = deltaVelocity * (_force * ForcePower * Time.fixedDeltaTime);
            _rb.AddForce(moveForce);
        }
    }

    private void MoveTo(Vector2 direction) 
    {
        this.direction = direction;
    }

    private IEnumerator StartDelay()
    {
        _canMove = false;
        yield return new WaitForSeconds(_startDelay);
        _canMove = true;
    }

    private IEnumerator ThrowItemCoroutine()
    {
        while (!_eh.IsDowned)
        {
            // Check if the enemy is on screen
            if (IsEnemyOnScreen())
            {
                // Check distance to player
                float distanceToPlayer = Vector2.Distance(transform.position, _playerRef.position);
                if (distanceToPlayer >= _minThrowDistance)
                {
                    yield return new WaitForSeconds(Random.Range(2f, 4f)); // Random interval between 2 to 4 seconds
                    ThrowItem();
                }
                else
                {
                    // Wait a bit before checking again
                    yield return new WaitForSeconds(1f);
                }
            }
            else
            {
                // Wait a bit before checking again
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private bool IsEnemyOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }


    private void ThrowItem()
    {
        if (_itemPrefab != null)
        {
            GameObject item = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            Vector2 throwDirection = (_playerRef.position - transform.position).normalized;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg;

            // Rotate the item to face the player
            item.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();
            itemRb.AddForce(throwDirection * ForcePower, ForceMode2D.Impulse);
        }
    }
}
