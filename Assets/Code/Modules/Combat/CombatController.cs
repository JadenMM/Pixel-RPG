using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum CombatState
{
    IDLE,
    SWINGING
}

public class CombatController : MonoBehaviour
{
    public Player Player;

    public CombatState CombatState;

    private void Update()
    {
        // Swing
        if (Input.GetKeyDown(KeyCode.Mouse0) && CombatState == CombatState.IDLE)
        {
            Swing();
        }
    }

    public void Swing()
    {
        CombatState = CombatState.SWINGING;
        Player.MovementController.Freeze(0.5f);
        UpdateState(CombatState.IDLE, 0.5f);

        Player.AnimationController.StartAnimation(AnimationGroup.SWING, loop: false);
    }

    private void UpdateState(CombatState state, float seconds)
    {
        StartCoroutine(UpdateStateTimer(state, seconds));
    }

    private IEnumerator UpdateStateTimer(CombatState state, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CombatState = state;
    }

}
