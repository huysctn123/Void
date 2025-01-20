using System;
using System.Collections;
using UnityEngine;


public class PlayerPotions : MonoBehaviour
{
    private Player player;

    public event Action OnManaUseChange;
    public event Action OnHealUseChange;
    public event Action OnManaUseZero;
    public event Action OnHealUseZero;

    [field: SerializeField] public int MaxManaPotionUses { get; private set; }
    [field: SerializeField] public int MaxHealPotionUses { get; private set; }

    public int CurrentHealPotionUseLeft 
    { get => currentHealPotionUseLeft; set
        {
            currentHealPotionUseLeft = Mathf.Clamp(value, 0, MaxHealPotionUses);
            if (currentHealPotionUseLeft == 0)
                OnHealUseZero?.Invoke();
        }
    }
    public int CurrentManaPotionUseLeft 
    { get => currentManaPotionUseLeft; set
        {
            currentManaPotionUseLeft = Mathf.Clamp(value, 0, MaxManaPotionUses);
            if (currentManaPotionUseLeft == 0)
                OnManaUseZero?.Invoke();
        }
    }
    private int currentHealPotionUseLeft;
    private int currentManaPotionUseLeft;

    private void Awake()
    {
        this.player = GetComponent<Player>(); ;
    }
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        currentHealPotionUseLeft = MaxHealPotionUses;
        currentManaPotionUseLeft = MaxManaPotionUses;
        OnManaUseChange?.Invoke();
        OnHealUseChange?.Invoke();
    }
    public void UseHealPotion()
    {
        currentHealPotionUseLeft -= 1;
        var amount = (player.Stats.Health.MaxValue * 40 /100);
        player.Stats.Health.Increase(amount);
        OnHealUseChange?.Invoke();
    }
    public void UseManaPotion()
    {
        currentManaPotionUseLeft -= 1;
        var amount = (player.Stats.Mana.MaxValue * 40 /100);
        player.Stats.Mana.Increase(amount);
        OnManaUseChange?.Invoke();
    }
    public void SetCurrentPotionValue(int HealTimeAmount, int ManaTimeAmount)
    {
        currentHealPotionUseLeft = HealTimeAmount;
        currentManaPotionUseLeft = ManaTimeAmount;
        OnManaUseChange?.Invoke();
        OnHealUseChange?.Invoke();
    }
}
