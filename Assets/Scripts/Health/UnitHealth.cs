using System;

namespace Assets.Scripts.Health
{
	public class UnitHealth
	{
		public UnitHealth(int maxAndStartHealth)
		{
			CurrentHealth = maxAndStartHealth;
			FullHealth = maxAndStartHealth;
		}

		public UnitHealth(int startHealth, int fullHealth)
		{
			CurrentHealth = startHealth;
			FullHealth = fullHealth;
		}

		public void Heal(int healAmount)
		{
			CurrentHealth += healAmount;
			CurrentHealth = Math.Min(CurrentHealth, FullHealth);
			CurrentHealth = Math.Max(CurrentHealth, 0);
		}

		public void TakeDamage(int damageAmount)
		{
			CurrentHealth -= damageAmount;
			CurrentHealth = Math.Min(CurrentHealth, FullHealth);
			CurrentHealth = Math.Max(CurrentHealth, 0);
		}

		public int FullHealth { get; set; }
		public int CurrentHealth { get; private set; }
	}
}
