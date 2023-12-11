using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            ReloadScene();
        }
    }

    public void ReloadScene()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }
}
