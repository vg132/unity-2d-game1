public class UnitHealth
{
	private int _health;

	public UnitHealth(int health)
	{
		_health = health;
	}

	public void TakeDamage(int damageAmount)
	{
		_health -= damageAmount;
	}

	public int Health => _health;
}
