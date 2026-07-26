using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class P_MoveState : State<Player>
{

    public override void FrameUpdate(Player player)
    {
        if(player.Movement.VelX==0)
        {
            player._Animator.Play("Idle");
        }
        else
        {
            player._Animator.Play("Move");
        }
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
    public override void EnterState(Player player)
    {
        if(player.Movement.isOnGround())
        {
            player.Movement.Fly();
        }
    }


    public override void FrameUpdate(Player player)
    {
        if(player.Movement.VelY>0)
        {
            player._Animator.Play("Fly");
        }
        else if(player.Movement.VelY<0)
        {
            player._Animator.Play("Fall");
        }
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
        if(player.Movement.isOnGround()&&player.Movement.VelY<=0f)
        {
            return player.moveState;
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

public class P_DigState : State<Player>
{
    public override void FrameUpdate(Player player)
    {
        player._Animator.Play("Dig");
        player.Movement.Move(player.InputDir.x);
        if (InputManager.Instance.GetKey(InputConstants.Action_Jump))
        {
            player.Movement.Fly();
        }
    }

    public override State<Player> ChangeState(Player player)
    {
        if(InputManager.Instance.GetKeyUp(InputConstants.Action_Dig))
        {
            if(player.Movement.isOnGround())
            {
                return player.moveState;
            }
            else
            {
                return player.airState;
            }
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
