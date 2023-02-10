using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : NetworkManager
{
    [SerializeField] private int pointsToWin = 3;
    private Dictionary<string, int> players = new Dictionary<string, int>();


    public GameEvent endGame;
    public string winnerName { get; private set; }

    private void PlayerCreate(string player)
    {
        players.Add(player, 0);
    }

    public void AddedPoint(string playerName)
    {
        if (!players.ContainsKey(playerName))
            PlayerCreate(playerName);

        players[playerName] += 1;
        CheckScore();
    }

    private void CheckScore()
    {
        Debug.Log(players.Keys);
        foreach (KeyValuePair<string, int> player in players)
        {
            if (player.Value >= pointsToWin)
            {
                Debug.Log($"{player} winner");
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        endGame.TriggerEvent();
    }

}
