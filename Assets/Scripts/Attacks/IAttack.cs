﻿public interface IAttack
{
    //What happenes when an enemy is hit.
    void OnHit(BaseCreature creatureHit, int instanceID);
}
