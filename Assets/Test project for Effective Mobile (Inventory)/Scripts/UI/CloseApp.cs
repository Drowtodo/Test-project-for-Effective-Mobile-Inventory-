using UnityEditor;
using UnityEngine;

public class CloseApp : MonoBehaviour
{
    public void Close()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
