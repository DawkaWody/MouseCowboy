using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackController : MonoBehaviour
{
    public Transform attackPoint;
    [SerializeField]
    private float _attackRadius;

    [SerializeField]
    private float _dashForce;
    [SerializeField]
    private float _dashCooldown;
    [SerializeField]
    private int _maxAttacks;
    [SerializeField]
    private float _reloadTime;

    [SerializeField]
    private int _attackStrength;

    [SerializeField] 
    private float _dashTime;

    [SerializeField]
    private UnityEvent _onDashBegin, _onDashEnded;

    private float _dashCounter;
    private bool _canDash = true;
    private int _attacks;
    private float _attackReloadCounter;
    private bool _reloading;

    private PlayerSpriteController _spriteController;
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _spriteController = GetComponent<PlayerSpriteController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dashCounter > 0 && !_canDash) 
        {
            _dashCounter -= Time.deltaTime;

            if (_dashCounter < 0 )
            {
                _canDash = true;
            }
        }

        if (_attacks < _maxAttacks && !_reloading)
        {
            _attackReloadCounter = _reloadTime;
            _reloading = true;
        }

        if (_reloading && _attackReloadCounter > 0)
        {
            _attackReloadCounter -= Time.deltaTime;

            if (_attackReloadCounter <= 0)
            {
                _attacks++;
                _reloading = false;
            }
        }
    }

    public void Dash()
    {
        if (_canDash)
        {
            _spriteController.BeginDash();
        }
    }

    public void FastDash()
    {
        _onDashBegin.Invoke();
        _rigidbody.AddForce(_spriteController.GetSpriteFacingVector() * _dashForce, ForceMode2D.Impulse);
        _dashCounter = _dashCooldown;
        _canDash = false;
        StartCoroutine(DashResetCo());
    }

    IEnumerator DashResetCo()
    {
        yield return new WaitForSeconds(_dashTime);
        _rigidbody.velocity = Vector2.zero;
        _onDashEnded.Invoke();
    }

    public void Attack()
    {
        if (_attacks > 0)
        {
            _spriteController.BeginAttack();
        }
    }
    
    public void AttackScratch()
    {
        Collider2D[] objectsHit = Physics2D.OverlapCircleAll(attackPoint.position, _attackRadius);

        foreach (Collider2D other in objectsHit) 
        {
            if (other.CompareTag("Player"))
            {

                other.GetComponent<PlayerHealthController>().Damage(_attackStrength, this.gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, _attackRadius);
    }
}
