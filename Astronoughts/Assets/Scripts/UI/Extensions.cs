using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Extensions
{
    public static class Ext
    {

    }

    public static class UIExt
    {
        public static void Reset()
        {
            Debug.LogWarning("Reset Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    // Object Stuff    
    // Methods for instantiating a gameObject

    public static class GameObjectExt
    {
        public static GameObject Generate(Vector3 position,GameObject gameObject)
        {
            return Object.Instantiate(gameObject,position,Quaternion.identity);
        }

        public static GameObject Generate(float xCoord,float yCoord,float zCoord,GameObject gameObject)
        {
            Vector3 position = new Vector3(xCoord,yCoord,zCoord);
            return Object.Instantiate(gameObject,position,Quaternion.identity);
        }

        public static GameObject Generate(float xCoord,float zCoord,GameObject gameObject)
        {
            Vector3 position = new Vector3(xCoord,0,zCoord);
            return Object.Instantiate(gameObject,position,Quaternion.identity);
        }

        // Methods for making an array of gameObjects visible

        public static void ToggleAllVisibility(GameObject[] objectArray,bool state)
        {
            foreach(GameObject objects in objectArray)
            {
                objects.GetComponent<MeshRenderer>().enabled = state;
            }
        }

        // Methods for making a gameObject and its children visible

        public static void ToggleAllVisibility(Transform gameObject,bool state)
        {
            for(int i = 0;i < gameObject.childCount;i++)
            {
                if(gameObject.GetChild(i).GetComponent<MeshRenderer>() != null)
                {
                    gameObject.GetChild(i).GetComponent<MeshRenderer>().enabled = state;
                }

                if(gameObject.GetChild(i).childCount > 0)
                {
                    ToggleAllVisibility(gameObject.GetChild(i),state);
                }
            }
        }

        // Methods for finding items with specified tag

        public static bool DoesTagExist(string tag)
        {
            bool exist = false;
            if(GameObject.FindGameObjectWithTag(tag) != null)
            {
                exist = true;
            }

            return exist;
        }

        // Methods for finding items with specified name

        public static bool DoesNameExist(string name)
        {
            bool exist = false;
            if(GameObject.Find(name) != null)
            {
                exist = true;
            }

            return exist;
        }

        // Methods for making an array of gameObjects active

        public static void SetAllActive(GameObject[] objectArray,bool state)
        {
            foreach(GameObject objects in objectArray)
            {
                objects.SetActive(state);
            }
        }

    }

    //Player Stuff
    public static class PlayerExt
    {
    // Player 3d movement
        public static void PlayerMovement(Vector3 movement,float speed,Rigidbody rigidbody)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            movement = new Vector3(moveHorizontal,0.0f,moveVertical);
            rigidbody.AddForce(movement * speed);
        }

        // Player 2d movement
        public static void PlayerRotation(Vector3 rotation,float speed,Rigidbody rigidbody)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            rotation = new Vector3(0.0f,0.0f,moveHorizontal);
        }

    }

    // Time stuff
    public static class TimeExt
    {
        // HH:MM:SS converted to seconds (SS)

        public static float ToSeconds(float hours,float minutes,float seconds)
        {
            float timer = seconds + (minutes * 60) + (hours * 3600);
            return timer;
        }

        // MM:SS converted to seconds (SS)

        public static float ToSeconds(float minutes,float seconds)
        {
            float timer = seconds + (minutes * 60);
            return timer;
        }

        // A game timer
        public static float Timer(float timer)
        {
            float seconds, minutes;

            timer += Time.deltaTime;
            minutes = Mathf.Floor(timer / 60);
            seconds = timer % 60;
            //return minutes.ToString("00") + ":" + seconds.ToString("00.00");
            return timer;
        }

        /* float Timer(float time, int speed)
         * Recurrsive timer with speed modifier.
         * REUSABLE
        */
        public static float Timer(float time,float speed)
        {
            time += Time.deltaTime * speed;
            return time;
        }

        /* CountDownTimer(float time)
         * Recurrsive count down timer.
         * REUSABLE
        */
        public static float CountDownTimer(float time)
        {
            return time -= Time.deltaTime;
        }
    }

    /* Functions for SceneManagement */
    public static class SceneExt
    {
        /* NextScene()
     * Calls the next scene in the build.
     * REUSABLE
    */
        public static void NextScene()
        {
            int totalScenes = SceneManager.sceneCountInBuildSettings;
            int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;

            if(currentsceneIndex != totalScenes - 1)
            {
                SceneManager.LoadScene(currentsceneIndex + 1);
            }
            else
            {
                Debug.Log("On last Scene.");
            }
        }

        /* PreviousScene()
         * Calls the previous scene in the build.
         * REUSABLE
        */
        public static void PreviousScene()
        {
            int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;

            if(currentsceneIndex != 0)
            {
                SceneManager.LoadScene(currentsceneIndex - 1);
            }
            else
            {
                Debug.Log("On first Scene.");
            }
        }

        /* SelectScene(int sceneIndex)
         * Calls the scene in the build at sceneIndex.
         * REUSABLE OVERLOAD
        */
        public static void SelectScene(int sceneIndex)
        {
            int totalScenes = SceneManager.sceneCountInBuildSettings;

            if(sceneIndex >= 0 && sceneIndex < totalScenes)
            {
                Debug.Log("Loading scene: " + sceneIndex);
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                Debug.Log("There is no scene at built index " + sceneIndex + ".");
            }
        }

        /* SelectScene(string sceneName)
         * Calls the scene with name sceneName.
         * REUSABLE OVERLOAD
        */
        public static void SelectScene(string sceneName)
        {
            if(DoesSceneExist(sceneName))
            {
                Debug.Log("Loading scene: " + sceneName);
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("Scene " + sceneName + " does not exist.");
            }
        }

        /* bool DoesSceneExist(string sceneName)
         * Checks if scene sceneName exist in the build.
         * REUSABLE
        */
        public static bool DoesSceneExist(string sceneName)
        {
            int lastSlash;
            string currentName, currentScenePath;

            if(string.IsNullOrEmpty(sceneName))
            {
                return false;
            }

            for(int i = 0;i < SceneManager.sceneCountInBuildSettings;i++)
            {
                currentScenePath = SceneUtility.GetScenePathByBuildIndex(i);
                lastSlash = currentScenePath.LastIndexOf("/");
                currentName = (currentScenePath.Substring(lastSlash + 1,currentScenePath.LastIndexOf(".") - lastSlash - 1));

                if(sceneName == currentName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
