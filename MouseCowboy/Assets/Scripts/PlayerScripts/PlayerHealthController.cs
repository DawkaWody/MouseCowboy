using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            if (value <= 0) 
            {
                _health = 0;
                Die();
            }
            else
            {
                _health = value;
            }
            _healthBar.SetHealth(Health);
        }
    }

    private int _health;
    [SerializeField] private int _maxHealth;

    [SerializeField]
    private HealthBar _healthBar;

    [SerializeField]
    private float _knockbackStrength;
    [SerializeField]
    private float _knockbackTime;

    [SerializeField]
    private UnityEvent _onKnockbackBegin, _onKnockbackEnded;

    private PlayerSpriteController _spriteController;
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _spriteController = GetComponent<PlayerSpriteController>();
        _rigidbody = GetComponent<Rigidbody2D>();

        Health = _maxHealth;
        _healthBar.SetMaxHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int power, GameObject sender)
    { 
        _spriteController.Damage();
        Health -= power;
        Knockback(sender.transform);
    }

    private void Die()
    {
        DontDestroyOnLoad(gameObject);
        _spriteController.Hide();
        _healthBar.Hide();
        LevelLoader.instance.Load(GameManager.instance.scoreboardScene);
    }

    private void Knockback(Transform sender)
    {
        _onKnockbackBegin.Invoke();
        Vector2 direction = (transform.position - sender.position).normalized;
        _rigidbody.AddForce(direction * _knockbackStrength, ForceMode2D.Impulse);
        StartCoroutine(KnockbackResetCo());
    }

    IEnumerator KnockbackResetCo()
    {
        yield return new WaitForSeconds(_knockbackTime);
        _rigidbody.velocity = Vector2.zero;
        _onKnockbackEnded.Invoke();
    }
}
