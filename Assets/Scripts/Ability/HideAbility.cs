using RECode.REFramework;

public class HideAbility : Ability
{
    public HideAbility(AbilityData data) : base(data) { }

    public override void Execute(Player player)
    {
        player.isHide = true;
        StartCooldown();
    }

    public override void OnUnequip(Player player)
    {
        player.isHide = false; // 切换能力时强制退出隐身
    }
}