using UnityEngine;

//* ENEMY TEMPLATE *//
//This template will be taken by each enemy.
//This makes sure that each enemy does something once they reach the end of the path.
//Some enemies will do different things when they reach the end.

public interface IEnemy
{
    //What happens when the enemy reaches the end of the path.
    //  This is in an interface because some enemies will do something special when they reach the end
    //before they die, but every enemy will do something, such as calling lightning down on each unit.
    void ReachedEnd();
}
