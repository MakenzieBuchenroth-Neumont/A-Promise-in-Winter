using NUnit.Framework;
using UnityEngine;
using System;

public class DesignPatternTests {
    private GameObject gameVariablesObject;

    [SetUp]
    public void Setup() {
        gameVariablesObject = new GameObject("GameVariables");
        gameVariablesObject.AddComponent<GameVariables>();
    }

    [TearDown]
    public void TearDown() {
        if (gameVariablesObject != null) {
            UnityEngine.Object.DestroyImmediate(gameVariablesObject);
        }
    }

    [Test]
    public void DecoratorPattern_StatProvider_AppliesBuffsCorrectly() {
        // Arrange
        var baseStats = new MockStatProvider();
        var buffedStats = new BuffedStatProvider(baseStats, bonusHP: 50, attackMultiplier: 1.5f);

        // Act & Assert
        Assert.That(buffedStats.GetMaxHP(), Is.EqualTo(150), "HP should be increased by 50");
        Assert.That(buffedStats.GetAttack(), Is.EqualTo(15), "Attack should be multiplied by 1.5");
        Assert.That(buffedStats.GetMaxMP(), Is.EqualTo(100), "MP should remain unchanged");
    }

    [Test]
    public void SingletonPattern_GameVariables_ReturnsSameInstance() {
        // Arrange & Act
        var instance1 = GameVariables.Instance;
        var instance2 = GameVariables.Instance;

        // Assert
        Assert.That(instance1, Is.SameAs(instance2), "Singleton should return the same instance");
    }

    [Test]
    public void ObserverPattern_GameVariables_NotifiesOnChange() {
        // Arrange
        var variables = GameVariables.Instance;
        bool wasNotified = false;
        int notifiedId = -1;
        int notifiedValue = -1;

        variables.OnVariableChanged += (id, value) => {
            wasNotified = true;
            notifiedId = id;
            notifiedValue = value;
        };

        // Act
        variables.Set(1, 42);

        // Assert
        Assert.That(wasNotified, Is.True, "Should notify observers when value changes");
        Assert.That(notifiedId, Is.EqualTo(1), "Should notify with correct variable ID");
        Assert.That(notifiedValue, Is.EqualTo(42), "Should notify with correct value");
    }

    private class MockStatProvider : ICharacterStats {
        public int GetMaxHP() => 100;
        public int GetMaxMP() => 100;
        public int GetAttack() => 10;
        public int GetDefense() => 10;
        public int GetAgility() => 10;
        public int GetLuck() => 10;
    }
} 