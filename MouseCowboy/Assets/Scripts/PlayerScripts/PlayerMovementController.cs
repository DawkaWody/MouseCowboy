using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private int _playerIndex = 0;

    // Private Variables
    private Camera _mainCamera;
    private float horizontalInput, verticalInput;
    private Vector2 _screenBounds;

    // Internal Components
    private Rigidbody2D _rigidbody;

    private PlayerSpriteController _spriteController;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));

        _rigidbody = GetComponent<Rigidbody2D>();
        
        _spriteController = GetComponent<PlayerSpriteController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(horizontalInput, verticalInput);

    }

    public void SetHInput(float input)
    {
        horizontalInput = input;
    }

    public void SetVInput(float input) 
    {  
        verticalInput = input;
    }

    public int GetPlayerIndex()
    {
        return _playerIndex;
    }

    void Move(float horizontal, float vertical)
    {
        if (vertical < 0)
        {
            _spriteController.animationSide = 0;
        }
        else if (vertical > 0)
        {
            _spriteController.animationSide = 1;
        }
        else if (horizontal != 0)
        {
            _spriteController.animationSide = 2;
        }

        Vector2 normalizedDirection = _spriteController.CalculateAnimatorVector(new Vector2(horizontal, vertical));

        _spriteController.horizontalDirection = normalizedDirection.x;
        _spriteController.verticalDirection = normalizedDirection.y;
        

        Vector2 direction = new Vector2(horizontal, vertical) * _moveSpeed;
         _rigidbody.velocity = direction;

        CheckBoundaries();
    }

    void CheckBoundaries()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -_screenBounds.x, _screenBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, -_screenBounds.y, _screenBounds.y);
        transform.position = viewPos;
    }
}
