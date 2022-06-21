using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager gmInstance;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        if(gmInstance == null)
        {
            gmInstance = this;
        }
        else if(gmInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        //Persist this instance through level change.
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
