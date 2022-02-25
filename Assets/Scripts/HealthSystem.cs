public class HealthSystem
{
    private int maxHealth;
    private int health;

    public HealthSystem(int health)
    {
        this.maxHealth = health;
        this.health = health;
    }

    public int GetMaxHealth() => maxHealth;

    public int GetHealth() => health;

    public void Damage(int damageAmount)
    {
        if(health - damageAmount < 0)
        {
            health = 0;
            return;
        }

        health -= damageAmount;
    }

    public void Heal(int healAmount)
    {
        if (health + healAmount > maxHealth)
        {
            health = maxHealth;
            return;
        }

        health += healAmount;
    }

    public void SetHealth(int amount)
    {
        if (amount > maxHealth)
        {
            health = maxHealth;
            return;
        }

        health = amount;
    }

    public void SetMaxHealth(int amount) => maxHealth = amount;
}
