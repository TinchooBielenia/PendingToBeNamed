using UnityEngine;

//TP2 - Martin Bielenia
public abstract class BaseCanvas : MonoBehaviour
{
    public virtual void Open()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public virtual bool IsOpen => gameObject.activeSelf;
}
