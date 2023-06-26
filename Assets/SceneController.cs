using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static GameObject canvas3D;

    private void Start()
    {
        canvas3D = GameObject.Find("3D Canvas");
    }

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void LoadSceneAdd(string scene)
    {
        SceneManager.LoadScene(scene,LoadSceneMode.Additive);

        if (scene == "2D")
        {
            canvas3D.SetActive(false);
        }
    }

    public static void UnloadScene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);

        if (scene == "2D")
        {
            GameObject.Find("Camera Rig").GetComponent<CameraController>().ResetCamera();

            canvas3D.SetActive(true);
        }
    }

    public static void Quit()
    {
        Application.Quit();
    }

    public static bool IsSceneActive(string s)
    {
        return SceneManager.GetActiveScene().name == s;
    }
}
