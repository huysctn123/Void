using System.Collections;
using UnityEngine;
using Void.CoreSystem;

namespace Void.UI
{
    public class GetPlayerMana : VoidMonoBehaviour
    {
        private Player player;
        public Stats stats;

        private ManaBar manaBar;
        protected override void LoadComponents()
        {

            base.LoadComponents();
            player = GameObject.FindObjectOfType<Player>();
            stats = player.GetComponentInChildren<Stats>();
            this.manaBar = GetComponent<ManaBar>();

        }
        protected override void Start()
        {
            base.Start();
            manaBar.GetStats(stats);
        }
    }
    
}