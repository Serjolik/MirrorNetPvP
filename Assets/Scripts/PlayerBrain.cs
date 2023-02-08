using System.Collections;
using System.Collections.Generic;
using Mirror;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class PlayerBrain : NetworkBehaviour
{
    /// <summary>
    /// Coloring time
    /// </summary>
    [SerializeField] private float affectedTime = 3f;

    [Header("Colors")]
    /// <summary>
    /// player affected color
    /// </summary>
    [SerializeField] private Color affectedColor;

    private Player player;
    private PlayerBrain anotherPlayer;
    private PlayerMovement ourMovement;

    private GameController controller;

    public bool affected { get; private set; }

    private void Start()
    {
        string name = "Player" + Random.Range(100, 999);

        var playerRenderer = GetComponentsInChildren<Renderer>().ToList();
        player = new Player(name, playerRenderer);
        controller = GameObject.Find("GameManager").GetComponent<GameController>();
        affected = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        anotherPlayer = collision.gameObject.GetComponent<PlayerBrain>();

        if(anotherPlayer != null )
        {
            ourMovement = GetComponent<PlayerMovement>();
            if (!anotherPlayer.affected && ourMovement.inDash)
            {
                anotherPlayer.Bump();
                player.GivePoint();
                controller.Bump(player);
            }
        }

    }

    public void Bump()
    {
        affected = true;
        player.Paint(affectedColor);
        Invoke(nameof(affectedTimer), affectedTime);
    }

    private void affectedTimer()
    {
        player.Paint(player.playerColor);
        affected = false;
    }

}
