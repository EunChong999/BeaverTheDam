using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Manager;

public class ESC : Manager
{
    [SerializeField] float range = 1;
    [SerializeField] float speed = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneAnim.instance.AnimOn(true);
            StartCoroutine(SceneMove());
        }
    }
    IEnumerator SceneMove()
    {
        yield return new WaitForSeconds(0.8f);
        SceneMove(Scenes.Title);
    }
}
