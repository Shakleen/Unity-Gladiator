using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandlerMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private GameObject _instructionsScreen;

    public void ExitButton() => Application.Quit();

    public void PlayButton() => SceneManager.LoadScene(1);

    public void InstructionsButton() => SetScreenActive(instructions: true);

    public void BackButton() => SetScreenActive(main: true);

    private void SetScreenActive(bool main = false, bool instructions = false)
    {
        _mainScreen.SetActive(main);
        _instructionsScreen.SetActive(instructions);
    }
}
