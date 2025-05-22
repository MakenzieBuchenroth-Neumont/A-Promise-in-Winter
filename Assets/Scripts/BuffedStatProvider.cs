using UnityEngine;

public class BuffedStatProvider : IStatProvider {
	private readonly IStatProvider source;
	private readonly int bonusHP;
	private readonly float attackMultiplier;

	public BuffedStatProvider(IStatProvider source, int bonusHP = 0, float attackMultiplier = 1f) {
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
