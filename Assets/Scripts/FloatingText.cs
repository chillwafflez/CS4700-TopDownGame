using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active; // to know whether or not it should be displayed
    public GameObject go; // reference to gameobject
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
            return;

     // exp:    10    -      7    >      2  --> Hide
        if (Time.time - lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
