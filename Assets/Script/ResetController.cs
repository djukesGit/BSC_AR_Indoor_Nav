using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btn_Restart;
    private void Start()
    {
        btn_Restart.onClick.AddListener(ResetScene);
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
