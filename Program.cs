// Public delegate used to summon minions
public delegate void MinionDelegate(Character character);

// Interface IItem
public interface IItem
{
    void Apply(Character character);
    bool RemoveItem(Character character);
    string ToString(); 
}

// Abstract character class, which sets common parameters for both party members and minions
public abstract class Character
{
    public string Name { get; set; }
    public int MaxHitPoints { get; set; }
    public int CurrentHitPoints { get; set; }
    public int BaseDamage { get; set; }
    public int BaseArmor { get; set; }

    public Character(string name, int maxHitPoints, int baseDamage, int baseArmor)
    {
        Name = name;
        MaxHitPoints = maxHitPoints;
        CurrentHitPoints = maxHitPoints;
        BaseDamage = baseDamage;
        BaseArmor = baseArmor;
    }

    public int Attack() => BaseDamage;

    public int Defense() => BaseArmor;

    public void Heal(int amount)
    {
        CurrentHitPoints += amount;
        if (CurrentHitPoints > MaxHitPoints) CurrentHitPoints = MaxHitPoints;
    }

    public void ReceiveDamage(int damage)
    {
        int damageTaken = Math.Max(0, damage - BaseArmor);
        CurrentHitPoints -= damageTaken;
        if (CurrentHitPoints < 0) CurrentHitPoints = 0;
    }

    public override string ToString()
    {
        return $"Character: {Name} | HP: {CurrentHitPoints}/{MaxHitPoints} | Damage: {BaseDamage} | Armor: {BaseArmor}";
    }
}

// PartyMember class, which can use items and generate minions
public class PartyMember : Character
{
    private List<IItem> _inventory;
    private List<Minion> _activeMinions;

    public PartyMember(string name, int maxHitPoints, int baseDamage, int baseArmor) 
        : base(name, maxHitPoints, baseDamage, baseArmor)
    {
        _inventory = new List<IItem>();
        _activeMinions = new List<Minion>();
    }

    public void AddItem(IItem item)
    {
        _inventory.Add(item);
    }

    public void ApplyItems()
    {
        foreach (var item in _inventory)
        {
            item.Apply(this);
        }
    }

    public bool RemoveItem(IItem item)
    {
        if (_inventory.Contains(item))
        {
            bool removed = item.RemoveItem(this);  
            if (removed)
            {
                _inventory.Remove(item);  
                return true;
            }
        }
        return false;
    }

    public void AddMinion(Minion minion)
    {
        _activeMinions.Add(minion);
    }

    public override string ToString()
    {
        string result = base.ToString();
        result += "\n  Inventory:";
        foreach (var item in _inventory)
        {
            result += $"\n    {item}";
        }

        result += "\n  Minions:";
        if (_activeMinions.Count == 0)
        {
            result += "\n    No active minions";
        }
        else
        {
            foreach (var minion in _activeMinions)
            {
                result += $"\n    {minion}";
            }
        }

        return result;
    }
}

// Minion class, defines basic minion properties
public class Minion : Character
{
    public Minion(string name, int maxHitPoints, int baseDamage, int baseArmor) 
        : base(name, maxHitPoints, baseDamage, baseArmor) { }

    public override string ToString()
    {
        return $"Minion: {Name} | HP: {CurrentHitPoints}/{MaxHitPoints} | Damage: {BaseDamage} | Armor: {BaseArmor}";
    }
}

// Abstract class Weapon
public abstract class Weapon : IItem
{
    public string Name { get; set; }
    public int Damage { get; set; }
    protected MinionDelegate? _minionDelegate;  // Make delegate nullable

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
        _minionDelegate = null;  // No minion by default
    }

    public Weapon(string name, int damage, MinionDelegate? minionDelegate)
    {
        Name = name;
        Damage = damage;
        _minionDelegate = minionDelegate;
    }

    public virtual void Apply(Character character)
    {
        character.BaseDamage += Damage;
        
        // Invoke the minion delegate if set
        _minionDelegate?.Invoke(character);
    }

    // Elimina el daño aplicado al personaje
    public virtual bool RemoveItem(Character character)
    {
        if (character.BaseDamage >= Damage)
        {
            character.BaseDamage -= Damage;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Weapon: {Name} | Damage: {Damage}";
    }
}

// Sword class, inherits from Weapon class
public class Sword : Weapon
{
    public Sword() : base("Sword", 10) { }
}

// Axe class, inherits from Weapon class
public class Axe : Weapon
{
    public Axe() : base("Axe", 15) { }
}

// Specific magical weapon: SwordMagical, summons a minion using delegate
public class SwordMagical : Weapon
{
    public SwordMagical() : base("SwordMagical", 10) { }

    public SwordMagical(MinionDelegate minionDelegate) : base("SwordMagical", 10, minionDelegate) { }

    public override string ToString()
    {
        return $"Magical Sword: {Name} | Damage: {Damage}";
    }
}

// Specific magical weapon: AxeMagical, summons a minion using delegate
public class AxeMagical : Weapon
{
    public AxeMagical() : base("AxeMagical", 15) { }

    public AxeMagical(MinionDelegate minionDelegate) : base("AxeMagical", 15, minionDelegate) { }

    public override string ToString()
    {
        return $"Magical Axe: {Name} | Damage: {Damage}";
    }
}

// Abstract class Protection
public abstract class Protection : IItem
{
    public string Name { get; set; }
    public int Armor { get; set; }
    protected MinionDelegate? _minionDelegate;  // Make delegate nullable

    // Constructor which doesn't include a minion by default
    public Protection(string name, int armor)
    {
        Name = name;
        Armor = armor;
        _minionDelegate = null;  
    }

    public Protection(string name, int armor, MinionDelegate? minionDelegate)
    {
        Name = name;
        Armor = armor;
        _minionDelegate = minionDelegate;
    }

    public virtual void Apply(Character character)
    {
        character.BaseArmor += Armor;

        // Invoke the minion delegate if set
        _minionDelegate?.Invoke(character);
    }

    // Removes the effects of the applied item
    public virtual bool RemoveItem(Character character)
    {
        if (character.BaseArmor >= Armor)
        {
            character.BaseArmor -= Armor;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Protection: {Name} | Armor: {Armor}";
    }
}

// Shield class, inherits from Protection class
public class Shield : Protection
{
    public Shield() : base("Shield", 5) { }
}

// Helmet class, inherits from Protection class
public class Helmet : Protection
{
    public Helmet() : base("Helmet", 3) { }
}

// Specific magical protection: ShieldMagical, summons a minion using delegate
public class ShieldMagical : Protection
{
    public ShieldMagical() : base("ShieldMagical", 5) { }

    public ShieldMagical(MinionDelegate minionDelegate) : base("ShieldMagical", 5, minionDelegate) { }

    public override string ToString()
    {
        return $"Magical Shield: {Name} | Armor: {Armor}";
    }
}

// Minion creation using delegate functions
public class MinionCreator
{
    public static void CreateEarthElemental(Character character)
    {
        var minion = new Minion("Earth Elemental", 30, 15, 5);
        Console.WriteLine($"{character.Name} summons {minion.Name}.");
        if (character is PartyMember partyMember)
        {
            partyMember.AddMinion(minion);
        }
    }

    public static void CreateFireElemental(Character character)
    {
        var minion = new Minion("Fire Elemental", 40, 20, 3);
        Console.WriteLine($"{character.Name} summons a {minion.Name}.");
        if (character is PartyMember partyMember)
        {
            partyMember.AddMinion(minion);
        }
    }

    public static void CreateIceElemental(Character character)
    {
        var minion = new Minion("Ice Elemental", 35, 25, 2);
        Console.WriteLine($"{character.Name} summons a {minion.Name}.");
        if (character is PartyMember partyMember)
        {
            partyMember.AddMinion(minion);
        }
    }
}

// Main program
public class Program
{
    public static void Main(string[] args)
    {
        // Creating a PartyMember
        PartyMember hero = new PartyMember("Hero", 100, 20, 5);

        // Creating magical weapons and protections that summon minions using delegates
        IItem magicalSword = new SwordMagical(MinionCreator.CreateIceElemental);
        IItem magicalAxe = new AxeMagical(MinionCreator.CreateFireElemental);
        IItem magicalShield = new ShieldMagical(MinionCreator.CreateEarthElemental);

        // Adding items to hero's inventory
        hero.AddItem(magicalSword);
        hero.AddItem(magicalAxe);
        hero.AddItem(magicalShield);

        // Applying items (which will summon minions)
        hero.ApplyItems();

        // Print the updated state
        Console.WriteLine(hero);

        // Removing items (and their effects)
        hero.RemoveItem(magicalSword);
        hero.RemoveItem(magicalShield);

        // Print the hero's state, including items and minions
        Console.WriteLine(hero);

        // Receiving damage and healing
        hero.ReceiveDamage(30);
        hero.Heal(20);

        // Print the updated state
        Console.WriteLine(hero);
    }
}

