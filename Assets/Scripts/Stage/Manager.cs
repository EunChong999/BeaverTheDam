using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public enum Scenes
    {
        Title,
        SelectScene,
        MainScene
    }

    public Scenes scenes;

    public virtual void SceneMove(Scenes moveScene)
    {
        SceneManager.LoadScene((int)moveScene);
    }
}
