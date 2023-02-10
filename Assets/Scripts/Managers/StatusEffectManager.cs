using System;

//This class is an event broker for causing status effects on enemies and units.
//When an attack causes a unit or enemy to be affected by a status, the attack will raise the proper status effect, which the enemies and units will be listening for.
public class StatusEffectManager
{
    public static Action<float, int> OnStun;
    public static Action<float, int> OnSlow;
    public static Action<float, int> OnShield;
}