namespace RECode.REFramework
{
    public abstract class Ability
    {
        public AbilityData Data { get; private set; }
        public float CooldownRemain { get; protected set; }
        public bool IsOnCooldown => CooldownRemain > 0;

        public Ability(AbilityData data)
        {
            Data = data;
        }

        // 每帧更新冷却
        public virtual void Tick(float deltaTime)
        {
            if (CooldownRemain > 0)
                CooldownRemain -= deltaTime;
        }

        // 核心：执行能力逻辑 —— 由子类实现
        public abstract void Execute(Player player);

        // 进入冷却
        public void StartCooldown()
        {
            CooldownRemain = Data.cooldown;
        }

        // 清理（切换能力时调用）
        public virtual void OnUnequip(Player player) { }
    }
}