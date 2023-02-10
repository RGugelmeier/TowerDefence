//* UNIT TEMPLATE *//
//This template will be taken by each unit.
//This makes sure that each unit does has a LevelUp function.
//Each unit will get different perks and stat changes when they level up.

//LevelUp(): What happens when the player levels up the unit from the level up menu.
//              This is in an interface because each unit will get different perks and stats buffs when they get leveld up,
//              and will even sometimes get a new ability at certain levels.

public interface IUnit
{
    void LevelUp();
}
