using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransitioner : MonoBehaviour
{
    // name of scene to transfer to
    public string transferSceneName;

    // array of game objects to bring along to the next scene
    public GameObject[] objectsToPreseve;
    
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
        // mark each preservable game object as such
        if(objectsToPreseve.Length > 0){
            foreach(GameObject obj in objectsToPreseve)
            {
                DontDestroyOnLoad(obj);
            }
        }

        // load the appropriate scene
        if(transferSceneName != null){
            Debug.Log("transitioning to scene " + transferSceneName);
            SceneManager.LoadScene(transferSceneName);
        }
    }
}
