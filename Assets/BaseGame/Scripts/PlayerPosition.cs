using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public float camareStartRotationX;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.registerPlayerPositions(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool selectPlayer(Player newPlayer)
    {
        if (player && player != newPlayer)
        {
            return false;
        }

        player = newPlayer;
        return true;
    }
}
