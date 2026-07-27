namespace RECode.REFramework
{
    public abstract class Ability
    {
        public AbilityData Data { get; private set; }
        public float CooldownRemain { get; protected set; }
        public bool IsOnCooldown => CooldownRemain > 0;

    }
}