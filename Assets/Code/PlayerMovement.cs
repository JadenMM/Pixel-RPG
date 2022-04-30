using UnityEngine;
using static PreferencesManager;

public class PlayerMovement : MonoBehaviour
{

    public Player Player;
    public Rigidbody2D Body;

    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {

        float x = 0;
        float y = 0;

        x += Input.GetKey(GetKeybind(GameAction.RIGHT)) ? 1 : 0;
        x -= Input.GetKey(GetKeybind(GameAction.LEFT)) ? 1 : 0;

        y += Input.GetKey(GetKeybind(GameAction.FORWARD)) ? 1 : 0;
        y -= Input.GetKey(GetKeybind(GameAction.BACK)) ? 1 : 0;

        movement = new Vector2();

        movement.x += x;
        movement.y += y;

        movement *= Player.Speed;

        movement = Vector2.ClampMagnitude(movement, Player.Speed);


        if (x == 1)
            Player.AnimationController.StartAnimation(AnimationType.WALK_RIGHT);
        else if (x == -1)
            Player.AnimationController.StartAnimation(AnimationType.WALK_LEFT);
        else if (y == 1)
            Player.AnimationController.StartAnimation(AnimationType.WALK_UP);
        else if (y == -1)
            Player.AnimationController.StartAnimation(AnimationType.WALK_DOWN);

        if (movement.magnitude <= 0)
            Player.AnimationController.SetIdleAnimation();

    }

    void FixedUpdate()
    {
        Body.MovePosition(Body.position + movement * Time.fixedDeltaTime);
    }
}
