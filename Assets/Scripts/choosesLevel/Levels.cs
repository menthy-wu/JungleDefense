using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public GameObject loading;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Continue", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Continue()
    {

        loading.SetActive(false);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
