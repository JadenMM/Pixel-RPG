using System.Collections;
using UnityEngine;
using static PreferencesManager;

public class MovementController : MonoBehaviour
{

    public Player Player;
    public Rigidbody2D Body;

    private Vector2 movement;

    public int LastDirection = 1;

    public bool IsFrozen = false;

    // Update is called once per frame
    void Update()
    {

        float x = 0;
        float y = 0;


        if (!IsFrozen)
        {
            x += Input.GetKey(GetKeybind(GameAction.RIGHT)) ? 1 : 0;
            x -= Input.GetKey(GetKeybind(GameAction.LEFT)) ? 1 : 0;

            y += Input.GetKey(GetKeybind(GameAction.FORWARD)) ? 1 : 0;
            y -= Input.GetKey(GetKeybind(GameAction.BACK)) ? 1 : 0;
        }

        movement = new Vector2();

        movement.x += x;
        movement.y += y;

        movement *= Player.Speed;

        movement = Vector2.ClampMagnitude(movement, Player.Speed);


        // Animation Values
        if (!IsFrozen)
        {
            if (x == 1)
                LastDirection = 3; // RIGHT
            else if (x == -1) // LEFT
                LastDirection = 1;
            else if (y == 1) // UP
                LastDirection = 0;
            else if (y == -1) // DOWN
                LastDirection = 2;


            if (movement.magnitude <= 0)
                Player.AnimationController.SetIdleAnimation();
            else
                Player.AnimationController.StartAnimation(AnimationGroup.WALK, LastDirection);
        }

    }

    void FixedUpdate()
    {
        Body.MovePosition(Body.position + movement * Time.fixedDeltaTime);
    }

    public void Freeze(float seconds)
    {
        StartCoroutine(FreezeTimer(seconds));
    }

    private IEnumerator FreezeTimer(float seconds)
    {
        IsFrozen = true;
        yield return new WaitForSeconds(seconds);
        IsFrozen = false;
    }
}
