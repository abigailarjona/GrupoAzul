using System;
using System.Collections;
using Cinemachine;
using HealthSystem;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Inputs))]
    public class ShooterController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
        [SerializeField] private LayerMask aimColliderMask;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private AudioClip shootAudioClip;
        [SerializeField] private AudioClip[] bulletHitAudioClips;
        [SerializeField] private TrailRenderer bullet;
        [SerializeField] private ParticleSystem bulletSmoke;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private Rig rig;
        [SerializeField] private Transform crosshair;
        [SerializeField] private TextMeshProUGUI ammoCountText;
        [SerializeField] private int maxAmmoCount = 12;
        [SerializeField] private float reloadTime = 3f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private AudioClip reloadSound;

        private Animator _animator;
        private Inputs _playerInputs;
        private PlayerController _playerController;
        private Camera _mainCamera;
        private int _currentAmmoCount;
        private bool _canShoot;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerInputs = GetComponent<Inputs>();
            _playerController = GetComponent<PlayerController>();
            _currentAmmoCount = maxAmmoCount;
            _canShoot = true;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (_playerInputs.aim && _canShoot)
            {
                Vector3 mouseWorldPosition = Vector3.zero;
                Vector2 screenCenterPoint = new(Screen.width / 2f, Screen.height / 2f);
                Ray ray = _mainCamera.ScreenPointToRay(screenCenterPoint);
                if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderMask, QueryTriggerInteraction.Ignore))
                {
                    aimTarget.position = hit.point;
                    mouseWorldPosition = hit.point;
                }

                _playerController.SetCanMove(false);
                _animator.SetLayerWeight(1, 1);
                rig.weight = 1f;

                // Activar target de los constraint IK y mira de apuntado
                aimTarget.gameObject.SetActive(true);
                crosshair.gameObject.SetActive(true);

                // Activar camara virtual de apuntado
                aimVirtualCamera.gameObject.SetActive(true);

                // Desactivar la rotación del jugador al moverse
                _playerController.SetRotateOnMove(false);

                // Rotar el cuerpo del jugador en dirección de apuntado
                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 playerForwardDir = (worldAimTarget - transform.position).normalized;
                transform.forward = playerForwardDir;

                if (_playerInputs.shoot)
                {
                    StartCoroutine(SpawnBullet(hit));

                    AudioSource.PlayClipAtPoint(shootAudioClip, transform.position);
                    _playerInputs.shoot = false;

                    IDamageable damageableTarget = hit.transform.GetComponentInParent<IDamageable>();
                    if (damageableTarget != null)
                    {
                        if (hit.transform.TryGetComponent<Head>(out _))
                            damageableTarget.TakeDamage(transform, attackDamage * 3);
                        else
                            damageableTarget.TakeDamage(transform, attackDamage);
                    }

                    // Agregar fuerza de impacto al objeto disparado
                    if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody target))
                        target.AddForceAtPosition(ray.direction * 4000f, hit.point);

                    int index = Random.Range(0, bulletHitAudioClips.Length);
                    AudioSource.PlayClipAtPoint(bulletHitAudioClips[index], hit.point, 0.1f);

                    _currentAmmoCount -= 1;
                    ammoCountText.text = $"{_currentAmmoCount} / {maxAmmoCount}";
                    if (_currentAmmoCount <= 0)
                    {
                        StartCoroutine(Reload());
                    }
                }
            }
            else
            {
                _playerController.SetCanMove(true);
                _animator.SetLayerWeight(1, 0);
                rig.weight = 0f;
                // Desactivar IK target y mira de apuntado
                aimTarget.gameObject.SetActive(false);
                crosshair.gameObject.SetActive(false);

                // Desactivar camara virtual de apuntado
                aimVirtualCamera.gameObject.SetActive(false);

                // Activar la rotacio del jugador al moverse 
                _playerController.SetRotateOnMove(true);
            }
        }

        private IEnumerator Reload()
        {
            _canShoot = false;
            yield return new WaitForSeconds(1f);
            SoundManager.Instance.PlayOneShot(reloadSound);
            ammoCountText.text = $"Reloading...";
            yield return new WaitForSeconds(reloadTime);
            _currentAmmoCount = maxAmmoCount;
            ammoCountText.text = $"{_currentAmmoCount} / {maxAmmoCount}";
            _canShoot = true;
        }

        private IEnumerator SpawnBullet(RaycastHit raycastHit)
        {
            TrailRenderer bulletTrail = Instantiate(bullet, bulletSpawnPoint.transform.position,
                quaternion.identity);
            float time = 0f;
            Vector3 startPos = bulletTrail.transform.position;
            while (time < 1f)
            {
                bulletTrail.transform.position = Vector3.Lerp(startPos, raycastHit.point, time);
                time += Time.deltaTime * 10f;
                yield return null;
            }

            Destroy(bulletTrail.gameObject);

            Instantiate(bulletSmoke, raycastHit.point, Quaternion.Euler(raycastHit.normal));
        }
    }
}