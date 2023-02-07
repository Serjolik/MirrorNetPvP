using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Coloring time
    /// </summary>
    [SerializeField] private float affectedTime = 3f;

    [Header("Rendering and colors")]
    /// <summary>
    /// player renderer component
    /// </summary>
    [SerializeField] private Renderer playerRenderer;
    /// <summary>
    /// player start color
    /// </summary>
    [SerializeField] private Color playerColor;
    /// <summary>
    /// player affected color
    /// </summary>
    [SerializeField] private Color affectedColor;

    private Player anotherPlayer;
    private PlayerMovement ourMovement;
    private int points = 0;

    public bool affected { get; private set; }

    void Start()
    {
        playerRenderer.material.color = playerColor;
        affected = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        anotherPlayer = collision.gameObject.GetComponent<Player>();

        if(anotherPlayer != null )
        {
            ourMovement = GetComponent<PlayerMovement>();
            if (!anotherPlayer.affected && ourMovement.inDash)
            {
                anotherPlayer.Bump();
                points++;
            }
        }

    }

    public void Bump()
    {
        affected = true;
        playerRenderer.material.color = affectedColor;
        Invoke(nameof(affectedTimer), affectedTime);
    }

    private void affectedTimer()
    {
        playerRenderer.material.color = playerColor;
        affected = false;
    }

}
