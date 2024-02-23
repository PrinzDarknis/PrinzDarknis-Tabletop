using System.Collections.Generic;

using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instanze;

    private static List<PlayerPosition> playerPositions = new List<PlayerPosition>();

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.instanze = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void registerPlayerPositions(PlayerPosition playerPosition)
    {
        playerPositions.Add(playerPosition);
    }

    public static PlayerPosition selectPlayerPosition(Player player)
    {
        foreach (PlayerPosition pos in playerPositions)
        {
            if (pos.selectPlayer(player))
            {
                return pos;
            }
        }

        return null;
    }
}
