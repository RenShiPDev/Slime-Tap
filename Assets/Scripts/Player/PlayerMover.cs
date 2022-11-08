using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerMover : MonoBehaviour
{
    public UnityEvent onClickEvent;

    [SerializeField] private GameObject _mouth;
    [SerializeField] private GameObject _fractures;
    [SerializeField] private GameObject _modelObject;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _backButton;

    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private SoundPlayer _soundPlayer;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _maxScaleY;

    private List<GameObject> _skins = new List<GameObject>();
    private Queue<MoveTrigger> _moveTriggers = new Queue<MoveTrigger>();
    private MoveTrigger _currentTrigger;
    private Rigidbody _rigidbody;
    private Quaternion _targetRotation;

    private float _lastTriggerMagnitude;
    private float _currentTriggerMagnitude;
    private float _movementBoost;
    private float _startScaleY;
    private float _minHeight = 0.55f;

    private bool _isScaled;
    private bool _isDied = false;

    private void Start()
    {
        _soundPlayer.SetPlayerHandler(ref onClickEvent);

        _targetRotation = transform.rotation;
        _rigidbody = GetComponent<Rigidbody>();

        _movementBoost = PlayerPrefs.GetInt("CurrentLevel");

        for (int i = 1; i <= 9; i++)
        {
            _skins.Add((GameObject)Resources.LoadAll("Skins/" + i + "/", typeof(GameObject))[0]);
        }

        int skinId = 0;
        string skinName = PlayerPrefs.GetString("CurrentSkin");
        foreach (var skin in _skins)
        {
            if(skinName == skin.name)
                break;

            skinId++;
        }
        if (skinId == _skins.Count) skinId--;

        var clone = Instantiate(_skins[skinId], _modelObject.transform);
        clone.transform.localPosition = Vector3.zero;

        Material material = clone.GetComponent<GetMaterial>().GetObjectMaterial();
        for(int i = 0; i < _fractures.transform.childCount; i++)
        {
            _fractures.transform.GetChild(i).GetComponent<MeshRenderer>().material = material;
        }

        _startScaleY = _modelObject.transform.localScale.y;
        SetTriggers();
    }

    private void FixedUpdate()
    {
        if (!_isDied)
        {
            MoveForward();
        }
    }

    private void Update()
    {
        if (_targetRotation != transform.rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
        }

        if (Input.GetMouseButtonDown(0) && !_isDied)
        {
            ActivateTrigger();
            onClickEvent.Invoke();
        }

        CheckTriggerDestination();
    }

    public void ChangeTargetRotation(int direction)
    {
        Vector3 directRotation = new Vector3(0, direction * 90, 0);
        _targetRotation = Quaternion.Euler(_targetRotation.eulerAngles + directRotation);
    }

    public void ChangeVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    private float Sigmoid(float value)
    {
        return (1f / (1f + (float)Mathf.Exp(-value))) / 1000f + 1f;
    }

    private void MoveForward()
    {
        if (transform.position.y < _minHeight)
        {
            _isDied = true;
            PlayerPrefs.SetInt("CurrentScore", 0);
            Die();
        }

        Vector3 direction = (_mouth.transform.position - transform.position) * Sigmoid(_movementBoost) * _movementSpeed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;

        if (!_isScaled)
        {
            if (_modelObject.transform.localScale.y < _maxScaleY)
                _modelObject.transform.localScale += new Vector3(0, _scaleSpeed * Time.deltaTime, 0);
            else
                _isScaled = true;
        }
        else
        {
            if (_modelObject.transform.localScale.y > _startScaleY)
                _modelObject.transform.localScale -= new Vector3(0, _scaleSpeed * Time.deltaTime, 0);
            else
                _isScaled = false;
        }
    }

    private void Die()
    {
        if (_fractures != null)
        {
            _fractures.SetActive(true);
            _modelObject.SetActive(false);

            for (int i = 0; i < _fractures.transform.childCount; i++)
            {
                var child = _fractures.transform.GetChild(i);
                child.GetComponent<Rigidbody>().velocity = child.transform.localPosition * _movementSpeed;
            }
            _fractures.transform.SetParent(transform.parent);
            _fractures = null;
        }

        _rigidbody.isKinematic = true;

        _restartButton.SetActive(true);
        _backButton.SetActive(true);
        this.enabled = false;
    }

    private void ActivateTrigger()
    {
        _currentTrigger.InvokeEvent();
        if(_moveTriggers.Count >= 1)
        {
            _currentTrigger = _moveTriggers.Dequeue();
            _lastTriggerMagnitude = (transform.position - _currentTrigger.transform.position).magnitude;
        }
    }

    private void SetTriggers()
    {
        var triggers = _levelGenerator.GetTriggers();
        _moveTriggers.Clear();
        foreach (var trigger in triggers)
        {
            _moveTriggers.Enqueue(trigger.GetComponent<MoveTrigger>());
        }
        _currentTrigger = _moveTriggers.Dequeue();
        _lastTriggerMagnitude = (transform.position - _currentTrigger.transform.position).magnitude;
        _movementBoost = PlayerPrefs.GetInt("CurrentLevel");
    }

    private void CheckTriggerDestination()
    {
        _currentTriggerMagnitude = (transform.position - _currentTrigger.transform.position).magnitude;
        if (_currentTriggerMagnitude > _lastTriggerMagnitude)
        {
            _currentTrigger.Die();

            if (_moveTriggers.Count > 0)
                _currentTrigger = _moveTriggers.Dequeue();
        }

        _lastTriggerMagnitude = _currentTriggerMagnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FinishTrigger>() != null)
        {
            SetTriggers();
        }
    }
}
