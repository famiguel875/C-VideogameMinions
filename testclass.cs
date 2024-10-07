using Xunit;

public class PartyMemberTests
{
    [Fact]
    public void CharacterCreation_SetsPropertiesCorrectly()
    {
        // Arrange
        string name = "Test PartyMember";
        int maxHP = 100;
        int baseDamage = 20;
        int baseArmor = 10;

        // Act
        PartyMember hero = new PartyMember(name, maxHP, baseDamage, baseArmor);

        // Assert
        Assert.Equal(name, hero.Name);
        Assert.Equal(maxHP, hero.MaxHitPoints);
        Assert.Equal(maxHP, hero.CurrentHitPoints);  // Current should match Max initially
        Assert.Equal(baseDamage, hero.BaseDamage);
        Assert.Equal(baseArmor, hero.BaseArmor);
    }

    [Fact]
    public void PartyMemberAttack_ReturnsBaseDamage()
    {
        // Arrange
        PartyMember warrior = new PartyMember("Warrior", 100, 30, 10);

        // Act
        int damage = warrior.Attack();

        // Assert
        Assert.Equal(30, damage);
    }

    [Fact]
    public void PartyMemberDefense_ReturnsBaseArmor()
    {
        // Arrange
        PartyMember warrior = new PartyMember("Warrior", 100, 30, 10);

        // Act
        int armor = warrior.Defense();

        // Assert
        Assert.Equal(10, armor);
    }

    [Fact]
    public void PartyMemberReceivesDamage_ReducesCurrentHitPoints()
    {
        // Arrange
        PartyMember warrior = new PartyMember("Warrior", 100, 30, 10);
        
        // Act
        warrior.ReceiveDamage(40);  // 40 damage - 10 armor = 30 damage taken

        // Assert
        Assert.Equal(70, warrior.CurrentHitPoints);
    }

    [Fact]
    public void PartyMemberHeals_IncreasesCurrentHitPoints()
    {
        // Arrange
        PartyMember warrior = new PartyMember("Warrior", 100, 30, 10);
        warrior.ReceiveDamage(50);

        // Act
        warrior.Heal(20);

        // Assert
        Assert.Equal(80, warrior.CurrentHitPoints);  // 40 damage, then healed 20
    }

    [Fact]
    public void PartyMemberHealingCannotExceedMaxHitPoints()
    {
        // Arrange
        PartyMember warrior = new PartyMember("Warrior", 100, 30, 10);
        warrior.ReceiveDamage(20);

        // Act
        warrior.Heal(50);  // Healing more than necessary

        // Assert
        Assert.Equal(100, warrior.CurrentHitPoints);  // Should not exceed max HP
    }
    [Fact]
    public void PartyMember_ApplyItems_IncreasesCharacterStats()
    {
        // Arrange
        PartyMember hero = new PartyMember("Hero", 100, 20, 5);
        IItem sword = new SwordMagical();
        IItem shield = new ShieldMagical();

        hero.AddItem(sword);
        hero.AddItem(shield);

        // Act
        hero.ApplyItems();

        // Assert
        Assert.Equal(30, hero.BaseDamage); // Sword adds 10
        Assert.Equal(10, hero.BaseArmor);  // Shield adds 5
    }

    [Fact]
    public void PartyMember_ApplyItems_SummonsMinions()
    {
        // Arrange
        PartyMember hero = new PartyMember("Hero", 100, 20, 5);
        IItem magicalSword = new SwordMagical(MinionCreator.CreateSpectralWarrior);

        hero.AddItem(magicalSword);

        // Act
        hero.ApplyItems();

        // Assert
        Assert.Contains("Spectral Warrior", hero.ToString());
    }

    [Fact]
    public void PartyMember_RemoveItem_RevertsCharacterStats()
    {
        // Arrange
        PartyMember hero = new PartyMember("Hero", 100, 20, 5);
        IItem magicalSword = new SwordMagical();
        IItem magicalShield = new ShieldMagical();

        hero.AddItem(magicalSword);
        hero.AddItem(magicalShield);
        hero.ApplyItems();

        // Act
        hero.RemoveItem(magicalSword);  // Remove sword
        hero.RemoveItem(magicalShield);  // Remove shield

        // Assert
        Assert.Equal(20, hero.BaseDamage);  // Damage should be back to original
        Assert.Equal(5, hero.BaseArmor);   // Armor should be back to original
    }
}

public class WeaponTests
{
    [Fact]
    public void Weapon_Apply_IncreasesCharacterDamage()
    {
        // Arrange
        var character = new PartyMember("Test Character", 100, 20, 5);
        var sword = new SwordMagical();

        // Act
        sword.Apply(character);

        // Assert
        Assert.Equal(30, character.BaseDamage);  // Sword adds 10 damage
    }

    [Fact]
    public void Weapon_Apply_SummonsMinion()
    {
        // Arrange
        var partyMember = new PartyMember("Test PartyMember", 100, 20, 5);
        var magicalSword = new SwordMagical(MinionCreator.CreateSpectralWarrior);

        // Act
        magicalSword.Apply(partyMember);

        // Assert
        Assert.Contains("Spectral Warrior", partyMember.ToString());
    }
}

public class MinionTests
{
    [Fact]
    public void MinionCreation_SetsPropertiesCorrectly()
    {
        // Arrange
        string name = "Fire Elemental";
        int maxHP = 40;
        int baseDamage = 20;
        int baseArmor = 3;

        // Act
        Minion minion = new Minion(name, maxHP, baseDamage, baseArmor);

        // Assert
        Assert.Equal(name, minion.Name);
        Assert.Equal(maxHP, minion.MaxHitPoints);
        Assert.Equal(maxHP, minion.CurrentHitPoints);
        Assert.Equal(baseDamage, minion.BaseDamage);
        Assert.Equal(baseArmor, minion.BaseArmor);
    }
}