using System;
using System.Collections;
using Items;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform player;
    [SerializeField] private Transform secondPartStartPos;
    [SerializeField] private Transform secondPartInvisibleWall;

    public static Action OnFirstPartCompleted;
    public static Action OnSecondPartCompleted;

    private void OnEnable()
    {
        OnFirstPartCompleted += GameManager_FirstPartCompleted;
        OnSecondPartCompleted += GameManager_OnSecondPartCompleted;
    }

    private void OnDisable()
    {
        OnFirstPartCompleted -= GameManager_FirstPartCompleted;
        OnSecondPartCompleted -= GameManager_OnSecondPartCompleted;
    }

    private void GameManager_FirstPartCompleted()
    {
        StartCoroutine(ShowFirstPartCompletedMessage());
    }

    private void GameManager_OnSecondPartCompleted()
    {
        StartCoroutine(ShowGameCompletedMessage());
    }

    private IEnumerator ShowFirstPartCompletedMessage()
    {
        playerInput.enabled = false;
        yield return new WaitForSeconds(3f);
        uiManager.ToggleFirstPartCompleted(true);

        CharacterController characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        player.position = secondPartStartPos.position;
        characterController.enabled = true;
        secondPartInvisibleWall.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);
        uiManager.ToggleFirstPartCompleted(false);

        UIManager.ShowMessage("Encuentra la ultima parte de la nave");
        yield return new WaitForSeconds(5f);
        UIManager.HideMessage();

        playerInput.enabled = true;
    }

    private IEnumerator ShowGameCompletedMessage()
    {
        yield return new WaitForSeconds(8f);
        uiManager.MostrarHasGanado();

        playerInput.enabled = false;
        yield return new WaitForSeconds(5f);
        playerInput.enabled = true;
        SceneManager.LoadScene(0);
    }
}