using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class P_MoveState : State<Player>
{
    public override void FrameUpdate(Player type)
    {
        Debug.Log("Move");
    }
    public override void PhysicsUpdate(Player player)
    {
        player.Movement.Move(player.InputDir.x);
    }

    public override State<Player> ChangeState(Player player)
    {
        if(InputManager.Instance.GetKeyDown(InputConstants.Action_Jump))
        {
            return player.airState;
        }
        else if (InputManager.Instance.GetKeyDown(InputConstants.Action_Dig))
        {
            return player.digState;
        }
        else
        {
            return null;
        }
    }
}

public class P_AirState : State<Player>
{
    public override void FrameUpdate(Player type)
    {
        Debug.Log("Fly");
    }

    public override void PhysicsUpdate(Player player)
    {
        if(InputManager.Instance.GetKey(InputConstants.Action_Jump))
        {
            player.Movement.Fly();
        }
        player.Movement.Move(player.InputDir.x);
    }

    public override State<Player> ChangeState(Player player)
    {
        if(player.Movement.isOnGround()&&player.Movement.VelY<-0.1f)
        {
            return player.moveState;
        }
        else
        {
            return null;
        }
    }
}

public class P_DigState : State<Player>
{
    public override void FrameUpdate(Player player)
    {
        player.DigBlock();
    }

    public override State<Player> ChangeState(Player player)
    {
        if(InputManager.Instance.GetKeyUp(InputConstants.Action_Dig)||player.InputDir!=Vector2.zero)
        {
            if(InputManager.Instance.GetKeyDown(InputConstants.Action_Jump))
            {
                return player.airState;
            }
            return player.moveState;
        }
        else
        {
            return null;
        }
    }
}

public class P_HideState:State<Player>
{

}
