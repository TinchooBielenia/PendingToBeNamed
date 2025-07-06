using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarSelector : MonoBehaviour
{
    public void SelectCharacter(int id)
    {
        GameManager.Instance.SetSelectedCharacter(id);
        SceneManager.LoadScene("03_Gameplay");
    }
}
