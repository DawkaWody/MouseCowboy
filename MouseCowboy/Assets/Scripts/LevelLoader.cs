using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    public Animator transition;
    [SerializeField]
    private float _transitionTime = 1.30f;

    public static LevelLoader instance {  get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void Load(string SceneName)
    {
        StartCoroutine(LoadCo(SceneName));
    }

    IEnumerator LoadCo(string SceneName)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(SceneName);
    }
}
