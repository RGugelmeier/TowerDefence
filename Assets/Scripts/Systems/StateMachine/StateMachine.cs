using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //This list will hold all of the possible states in the state machine.
    private List<BaseState> states;
    //The state that is currently active. This only displays the active top level state, so if the active state is a composite state, you must use the state's activeSubState variable to access the current bottom level state.
    BaseState activeState;
}
