using Assets.Scripts.Health;
using System;

namespace Assets.Scripts.Managers
{
	public class PlayerManager
	{
		private static PlayerManager _instance;

		private UnitHealth _health;
		private int _points;

		#region Events

		public static event Action<int> OnHealthChanged;
		public static event Action<int> OnPointsChanged;
		public static event Action OnDeath;

		#endregion

		public PlayerManager()
		{
			Initialize();
		}

		public static PlayerManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new PlayerManager();
				}
				return _instance;
			}
		}

		private void Initialize()
		{
			_health = new UnitHealth(10);
			_points = 0;
		}

		public void UpdatePoints(int pointsAmount)
		{
			if (pointsAmount != 0)
			{
				_points += pointsAmount;
				OnPointsChanged?.Invoke(_points);
			}
		}

		public void UpdateHealth(int damageAmount)
		{
			_health.TakeDamage(damageAmount);
			if (damageAmount != 0)
			{
				OnHealthChanged?.Invoke(_health.CurrentHealth);
				if (_health.CurrentHealth <= 0)
				{
					OnDeath?.Invoke();
				}
			}
		}

		public int Points => _points;
		public int CurrentHealth => _health.CurrentHealth;
	}
}
