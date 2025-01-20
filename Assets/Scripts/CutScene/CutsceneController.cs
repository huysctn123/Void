using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private Player player;   

    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
    }
    public void StartCutscene()
    {
        // Add your cutscene logic here
        player.enabled = false;
        Debug.Log("Cutscene started!");
        // For example, you might play an animation, disable player control, etc.
    }

    public void StopCutscene()
    {
        // Add logic to stop the cutscene if necessary
        Debug.Log("Cutscene stopped!");
    }
}