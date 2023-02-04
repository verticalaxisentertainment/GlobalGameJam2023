using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public int sceneIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (LevelManager.Instance.BookCam.Priority == 20)
        {
            SceneManager.LoadScene(1);
            //sceneIndex++;
        }
    }
}
