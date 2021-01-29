public enum UnitStat { MaxHP, MaxMP, HP, MP, Strength, Wisdom, Defend, MagicDefend }
public enum WeaponDamageType { Physic, Magical }
public enum ArmorType { Physic, Magical }

public enum TeamSide { Player, Enemy }
public enum BattleResult { Win, Lose, Retreat, Draw }

public enum BattleState { Start, Input, TurnProcess, EnemyTurn, Result }
public enum GameState { Battle, Event}

public enum TargetType { Self, OneEnemy, OneAlly, AllEnemies, AllAllies, AllAlliesButSelf, All }