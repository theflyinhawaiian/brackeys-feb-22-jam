namespace Assets.Scripts
{
    public interface IItemWithCooldown
    {
        float GetLastUseTime();
        float GetCooldown();
    }
}