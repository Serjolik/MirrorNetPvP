using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using System.Linq;
using UnityEngine;

public class PlayerBrain : NetworkBehaviour
{
    /// <summary>
    /// Coloring time
    /// </summary>
    [SerializeField] private float affectedTime = 3f;
    /// <summary>
    /// player affected color
    /// </summary>
    [SerializeField] private Color affectedColor;
    private Color playerColor = Color.white;

    private PlayerBrain anotherPlayer;
    private PlayerMovement ourMovement;

    private GameController controller;

    private TextMeshPro playerNameText;

    private List<Renderer> playerRenderer;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChange))]
    public Color thisColor;

    public bool affected = false;
    public bool isInDash = false;

    private void Start()
    {
        playerRenderer = GetComponentsInChildren<Renderer>().ToList();
        ourMovement = gameObject.GetComponent<PlayerMovement>();
        playerNameText = GetComponentInChildren<TextMeshPro>();
        controller = GameObject.Find("Managers").GetComponent<GameController>();
    }

    public override void OnStartLocalPlayer()
    {
        string name = "Player" + Random.Range(100, 999);
        CmdSetupPlayer(name);
        CmdChangeColor(playerColor);
    }


    void OnNameChanged(string _Old, string _New)
    {
        playerNameText = GetComponentInChildren<TextMeshPro>();
        playerNameText.text = playerName;
        
    }

    void OnColorChange(Color oldValue, Color newValue)
    {
        Debug.Log("OnAffectedChange");
        playerRenderer = GetComponentsInChildren<Renderer>().ToList();
        foreach (Renderer renderer in playerRenderer)
        {
            renderer.material.color = newValue;
        }
        thisColor = newValue;
    }

    public void CmdBump()
    {
        Debug.Log("CmdBump");
        StartCoroutine(affectedTimer());
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (affected)
            CmdChangeColor(thisColor);
        else
            CmdChangeColor(playerColor);
    }

    [Command]
    public void CmdOtherBumped()
    {
        controller.AddedPoint(playerName);
    }

    public IEnumerator affectedTimer()
    {
        Debug.Log("IEnumerator");
        affected = true;
        yield return new WaitForSeconds(affectedTime);
        affected = false;
    }

    [Command]
    public void CmdSetupPlayer(string _name)
    {
        playerName = _name;
    }
    [Command]
    public void CmdChangeColor(Color _newValue)
    {
        thisColor = _newValue;
    }

}
