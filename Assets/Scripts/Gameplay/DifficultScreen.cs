using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultScreen : MonoBehaviour
{
    /// <summary>
    /// Question #4
    /// Created scenes and this just make sure which ever one the player picks, it will go to that scene and the course and depending on the difficulty there will be more enemies
    /// </summary>

    public void EasyDiff()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("You chose the easy difficulty course!");
    }

    public void MediumDiff()
    {
        SceneManager.LoadScene("MidDiffCourse");
        Debug.Log("You chose the medium difficulty course!");
    }

    public void HardDiff()
    {
        SceneManager.LoadScene("HardDiffCourse");
        Debug.Log("You chose the hard difficulty course!");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
