using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string name { get; private set; }
    public int points { get; private set; }

    private List<Renderer> playerRenderer;

    // default color white
    public Color playerColor { get; private set; } = Color.white;

    public Player(string name, List<Renderer> playerRenderer)
    {
        this.name = name;
        this.playerRenderer = playerRenderer;
    }

    public void GivePoint()
    {
        points++;
    }
    public void Paint(Color color)
    {
        foreach (Renderer renderer in playerRenderer)
        {
            renderer.material.color = color;
        }
    }

}
