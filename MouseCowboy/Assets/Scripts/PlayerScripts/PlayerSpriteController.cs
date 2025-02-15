using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    public int animationSide;
    public float horizontalDirection;
    public float verticalDirection;

    [SerializeField]
    private Color _damageColor;
    [SerializeField]
    private Transform _healthBar;
    [SerializeField]
    private Transform _bulletBar;

    private Vector2 _spriteFacing;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteFacing = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontalDirection == 1)
        {
            transform.localScale = new Vector3(1f, 1f, 0f);
            _healthBar.localScale = new Vector3(1f, 1f, 0f);
            if (_bulletBar != null)
            {
                _bulletBar.localScale = new Vector3(1f, 1f, 0f);
            }

            _spriteFacing.x = 1f;

        }
        else if (horizontalDirection == -1) 
        {
            transform.localScale = new Vector3(-1f, 1f, 0f);
            _healthBar.localScale = new Vector3(-1f, 1f, 0f);
            if (_bulletBar != null)
            {
                _bulletBar.localScale = new Vector3(-1f, 1f, 0f);
            }

            _spriteFacing.x = -1f;
        }

        if (verticalDirection != 0)
        {
            _spriteFacing.y = verticalDirection;
        }

        _animator.SetFloat("horizontal", horizontalDirection);
        _animator.SetFloat("vertical", verticalDirection);
        
        _animator.SetFloat("side", animationSide);

        if (horizontalDirection != 0 || verticalDirection != 0)
        {
            _animator.SetBool("moving", true);
        }
        else
        {
            _animator.SetBool("moving", false);
        }
    }

    public void BeginShoot()
    {
        _animator.SetTrigger("shoot");
    }

    public void BeginDash()
    {
           _animator.SetTrigger("dash");
    }

    public void BeginAttack()
    {
        _animator.SetTrigger("attack");
    }

    public void Damage()
    {
        StartCoroutine(DamageCo());
    }

    public void Hide()
    {
        _spriteRenderer.enabled = false;
        if (_bulletBar != null)
        {
            _bulletBar.gameObject.SetActive(false);
        }
    }

    IEnumerator DamageCo()
    {
        _spriteRenderer.color = _damageColor;
        yield return new WaitForSeconds(.1f);
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public Vector2 CalculateAnimatorVector(Vector2 movementDirection)
    {
        if (Mathf.Abs(movementDirection.x) > 0 && Mathf.Abs(movementDirection.y) > 0)
        {
            movementDirection = new Vector2(Normalize(movementDirection.x), 0f);
        }
        else
        {
            movementDirection = new Vector2(Normalize(movementDirection.x), Normalize(movementDirection.y));
        }

        return movementDirection;
    }

    public Vector2 GetSpriteFacingVector()
    {
        return _spriteFacing;
    }

    public float Normalize(float value)
    {
        if (value < 0.0f)
        {
            return -1f;
        }
        else if (value > 0.0000f)
        {
            return 1f;
        }
        else
        {
            return 0f;
        }
    }
}