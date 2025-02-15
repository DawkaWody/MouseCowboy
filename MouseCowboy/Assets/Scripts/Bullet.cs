using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int attackStrength;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity == Vector2.zero)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mouse"))
        {
            other.GetComponent<PlayerHealthController>().Damage(attackStrength, this.gameObject);
        }

        if (!other.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        _rigidbody.velocity = Vector2.zero;
        _animator.SetTrigger("explode");
        Destroy(this.gameObject, .15f);
    }
}
