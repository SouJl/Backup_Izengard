namespace EnemyUnit
{
    public class EnemyModel
    {
        private readonly EnemyType _type;
        private readonly int _maxHealth;
        private readonly int _attackDamage;
        private readonly int _defeatCost;
        private int _currentHealth;

        public EnemyType Type => _type;
        public int AttackDamage => _attackDamage;
        public int DefeatCost => _defeatCost;

        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                if(_currentHealth != value)
                {
                    if (value > _maxHealth)
                        _currentHealth = _maxHealth;
                    else if (value < 0)
                        _currentHealth = 0;
                    else
                        _currentHealth = value;                   
                }
            }
        }

        public EnemyModel(EnemySettings data)
        {
            _type = data.Type;
            _maxHealth = data.MaxHelth;
            _attackDamage = data.AttackDamage;
            _defeatCost = data.DefeatCost;
           
            CurrentHealth = _maxHealth;
        }

        public void IncreaseHealth(int amount)
        {
            CurrentHealth += amount;
        }

        public void DecreaseHealth(int amount) 
        {
            CurrentHealth -= amount;
        }

    }
}
