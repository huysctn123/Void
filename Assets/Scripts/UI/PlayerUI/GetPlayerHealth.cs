using System.Collections;
using Void.CoreSystem;
using UnityEngine;
using Void.UI;


public class GetPlayerHealth : VoidMonoBehaviour
{
    private Player player;
    public Stats stats;

    private HealthBar healthBar;
    private ManaBar manaBar;
    protected override void LoadComponents()
    {
        player = GameObject.FindObjectOfType<Player>();
        stats = player.GetComponentInChildren<Stats>();
        this.healthBar = GetComponent<HealthBar>();

    }
    protected override void Start()
    {
        base.Start();
        healthBar.GetStats(stats);
    }
}
