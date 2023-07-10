using UnityEngine;

namespace Items
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private Scenario1Manager gameManager;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject exhaustL;
        [SerializeField] private GameObject exhaustR;

        private bool _hudNavigator;
        private bool _fluxCondensor;
        private bool _iaNeuralInterface;

        private void OnEnable()
        {
            SpaceshipPartContainer.onPartInstalled += InstallPart;
        }

        private void OnDisable()
        {
            SpaceshipPartContainer.onPartInstalled -= InstallPart;
        }

        private void InstallPart(SpaceshipPart.Id part)
        {
            switch (part)
            {
                case SpaceshipPart.Id.HudNavigator:
                    _hudNavigator = true;
                    break;
                case SpaceshipPart.Id.FluxCondensor:
                    _fluxCondensor = true;
                    break;
                case SpaceshipPart.Id.IaNeuralInterface:
                    _iaNeuralInterface = true;
                    break;
                default:
                    return;
            }

            CheckIfCompleted();
        }

        private void CheckIfCompleted()
        {
            if (_hudNavigator && _fluxCondensor && _iaNeuralInterface)
                StartFlying();
        }

        private void StartFlying()
        {
            audioSource.Play();
            exhaustL.SetActive(true);
            exhaustR.SetActive(true);
            animator.enabled = true;

            gameManager.OnScenarioFinished();
        }
    }
}