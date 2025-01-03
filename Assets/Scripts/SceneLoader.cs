using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(LoadPresentSceneWithDelay());
    }

    private IEnumerator LoadPresentSceneWithDelay()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(1);
    }
}
