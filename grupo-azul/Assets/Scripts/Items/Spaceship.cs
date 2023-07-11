using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject exhaustL;
        [SerializeField] private GameObject exhaustR;

        private readonly List<SpaceshipPart.Id> _installedParts = new List<SpaceshipPart.Id>();

        private void OnEnable()
        {
            SpaceshipPartContainer.OnSpaceshipPartInstalled += InstallPart;
        }

        private void OnDisable()
        {
            SpaceshipPartContainer.OnSpaceshipPartInstalled -= InstallPart;
        }

        private void InstallPart(SpaceshipPart.Id part)
        {
            _installedParts.Add(part);
            CheckIfCompleted();
        }

        private void CheckIfCompleted()
        {
            switch (_installedParts.Count)
            {
                case 2:
                    GameManager.OnFirstPartCompleted?.Invoke();
                    break;
                case 3:
                    StartFlying();
                    break;
            }
        }

        private void StartFlying()
        {
            audioSource.Play();
            exhaustL.SetActive(true);
            exhaustR.SetActive(true);
            animator.enabled = true;

            // Invocar el evento onSpaceShipFixed
            GameManager.OnSecondPartCompleted?.Invoke();
        }
    }
}