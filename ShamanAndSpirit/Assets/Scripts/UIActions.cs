using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIActions : MonoBehaviour
{
    [SerializeField]
    List<GameObject> m_ObjectsToAppear = null;

    public void AppearObject()
    {
        foreach (GameObject l_Object in m_ObjectsToAppear)
        {
            l_Object.SetActive(true);
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToScene(string p_SceneName)
    {
        SceneManager.LoadScene(p_SceneName);
    }
}
