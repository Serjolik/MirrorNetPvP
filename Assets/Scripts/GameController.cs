using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int pointsToWin = 3;
    private List<Player> players = new List<Player>();
    public GameEvent endGame;
    public string winnerName { get; private set; }

    public void Bump(Player player)
    {
        if (!players.Contains(player))
            PlayerCreate(player);

        if (player.points >= pointsToWin)
        {
            winnerName = player.name;
            EndGame();
        }
    }

    public void PlayerCreate(Player player)
    {
        players.Add(player);
    }

    private void EndGame()
    {
        endGame.TriggerEvent();
    }

}
