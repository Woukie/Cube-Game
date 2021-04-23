using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void QuitGame() {
        Application.Quit();
        Debug.Log("Quit game!");
    }
}
