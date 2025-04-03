using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;

    protected override void OnCollide(Collider2D other)
    {
        if (other.name == "Player")
        {
            // teleport the player
            GameManager.instance.SaveState();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
