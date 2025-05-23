using UnityEngine;

public class BuffedStatProvider : ICharacterStats {
	private readonly ICharacterStats source;
	private readonly int bonusHP;
	private readonly float attackMultiplier;

	public BuffedStatProvider(ICharacterStats source, int bonusHP = 0, float attackMultiplier = 1f) {
		this.source = source;
		this.bonusHP = bonusHP;
		this.attackMultiplier = attackMultiplier;
	}

	public int GetMaxHP() => source.GetMaxHP() + bonusHP;
	public int GetMaxMP() => source.GetMaxMP();
	public int GetAttack() => Mathf.RoundToInt(source.GetAttack() * attackMultiplier);
	public int GetDefense() => source.GetDefense();
	public int GetAgility() => source.GetAgility();
	public int GetLuck() => source.GetLuck();
}
