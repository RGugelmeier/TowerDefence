//* OBJECT POOL TEMPLATE *//
//This template will ensure that an object pool includes the VerifyPool function.

//VerifyPool: This function checks to make sure each object that can be created (each object in the objects list)
//              is a prefab that contains the proper script for this pool. For example, the enemy pool will check
//              that each game object inside of it's objects list, contains the BaseEnemy script as a component.

public interface IObjectPool
{
    bool VerifyPool();
}
