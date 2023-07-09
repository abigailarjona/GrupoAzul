using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenario1Manager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    public void OnScenarioFinished()
    {
        StartCoroutine(GoToMainMenu());
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(8f);
        uiManager.ActivarMenuHasGanado();

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}