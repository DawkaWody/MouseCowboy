using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private GameObject _bulletIcon1, _bulletIcon2, _bulletIcon3;

    [SerializeField]
    private float _shootingForce = 20f;
    [SerializeField]
    private int _maxBullets;
    [SerializeField]
    private float _reloadTime;
    [SerializeField]
    private float _minTimeBetweenShots;

    [SerializeField]
    private int _attackStrength;

    private int _bullets;
    private float _bulletReloadCounter;
    private float _shootingTimer;
    private bool _reloading;
    private bool _afterShot;

    private PlayerSpriteController _spriteController;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteController = GetComponent<PlayerSpriteController>();

        _bullets = _maxBullets;
        _shootingTimer = _minTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bullets < _maxBullets && !_reloading)
        {
            _bulletReloadCounter = _reloadTime;
            _reloading = true;
        }

        if (_reloading && _bulletReloadCounter > 0)
        {
            _bulletReloadCounter -= Time.deltaTime;

            if (_bulletReloadCounter <= 0)
            {
                _bullets++;
                _reloading = false;
            }
        }

        if (_afterShot)
        {
            _shootingTimer -= Time.deltaTime;
            if (_shootingTimer <= 0)
            {
                _afterShot = false;
                _shootingTimer = _minTimeBetweenShots;
            }
        }

        HandleBulletUI();
    }

    public void Shoot()
    {
        if (_bullets > 0)
        {
            _spriteController.BeginShoot();
        }
    }

    public void ShootBullet()
    {
        if (_afterShot) return;
        Vector2 shootingDirection = _spriteController.GetSpriteFacingVector();
        Debug.Log(shootingDirection);
        GameObject newBullet = Instantiate(_bulletPrefab, _firePoint.position, transform.rotation);
        newBullet.GetComponent<Bullet>().attackStrength = _attackStrength;
        newBullet.GetComponent<Rigidbody2D>().AddForce(shootingDirection * _shootingForce, ForceMode2D.Impulse);
        _bullets--;
        _afterShot = true;
    }

    private void HandleBulletUI()
    {
        switch (_bullets)
        {
            case 0:
                _bulletIcon1.SetActive(false);
                _bulletIcon2.SetActive(false);
                _bulletIcon3.SetActive(false);
                break;
            case 1:
                _bulletIcon1.SetActive(true);
                _bulletIcon2.SetActive(false);
                _bulletIcon3.SetActive(false);
                break;
            case 2:
                _bulletIcon1.SetActive(true);
                _bulletIcon2.SetActive(true);
                _bulletIcon3.SetActive(false);
                break;
            case 3:
                _bulletIcon1.SetActive(true);
                _bulletIcon2.SetActive(true);
                _bulletIcon3.SetActive(true);
                break;
        }
    }
}
