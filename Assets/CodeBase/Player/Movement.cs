using UnityEngine;

public class Movement : MonoCache
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _weight;
    [SerializeField] private float _maxWeight;
    // [SerializeField] private float _maximumTimeStep;

    // [SerializeField] private float _currentTimeStep;

    // [SerializeField] private AudioSource _audioSteps;
    private Vector2 _movement;
    private Vector2 _mousePosition;
    private int _index = 0;

    public Rigidbody2D Rb => _rb;
    public float MoveSpeed => _moveSpeed;    

    private void Awake()
    {
        _camera = Camera.main;
    }

    public override void OnFixedUpdateTick()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _movement = new Vector3(x, y, 0) * _moveSpeed;
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        // _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        if(_moveSpeed > 0)
            _rb.velocity = _movement;

        Vector2 lookDir = _mousePosition - _rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        _rb.rotation = angle;

        // if (x != 0 || y != 0)
        // {
        //     AudioClip[] clips = AudioEffects.Instance.AudioSteps;
        //     if (!_audioSteps.isPlaying)
        //     {
        //         _index++;
        //         if (_index >= clips.Length)
        //             _index = 0;

        //         _audioSteps.clip = clips[_index];
        //         _audioSteps.Play();
        //     }
        // }

        // if (_currentTimeStep > 0)
        //     _currentTimeStep -= Time.deltaTime;
    }

    public void AddSpeedMovement(float speed)
    {
        _moveSpeed += (_moveSpeed / 100) * speed;
    }

    public void SetSpeed(float speed)
    {
        _moveSpeed = speed;
    }
    
    private float _previousWeight; // Предыдущий вес
    private float _initialMoveSpeed; // Исходная скорость

    public void SetSpeedWeight(float weight)
    {    
        if (weight != _previousWeight && weight > 0 && weight < Mathf.Infinity) // Если вес изменился и не равен 0 или бесконечности
        {
            _weight -= _previousWeight;
            _weight += weight;
            if (_initialMoveSpeed == 0.0f) 
            {
                _initialMoveSpeed = _moveSpeed; // Сохраняем исходную скорость при первом вызове метода
            }

            var speed = _initialMoveSpeed;
            if (weight > _maxWeight) 
            {
                // Если персонаж перегружен, то снижаем скорость
                speed = _initialMoveSpeed * (_maxWeight / weight);
            } 

            _moveSpeed = speed;
            _previousWeight = weight; // Запоминаем новый вес
        }
    }
}