using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage the scene changing.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// Change the scene to the given index.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene that will be load.</param>
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
