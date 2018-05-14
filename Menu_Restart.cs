using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Restart : MonoBehaviour
{
    public int Scene = 0;
    void OnClick() // i really hope you dont need an explanation for this one
    {
        SceneManager.LoadScene(Scene);
    }
}
