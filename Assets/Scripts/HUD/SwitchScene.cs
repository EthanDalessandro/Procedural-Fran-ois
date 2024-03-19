using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SceneToSwitchTo(string name)
    {
        SceneManager.LoadScene(name);
    }
}
