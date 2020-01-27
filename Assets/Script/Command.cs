using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(out float direction, Animator anim);
}

#region Classes that deal with Movement and Animation

public class MoveUp : Command
{
    public override void Execute(out float direction, Animator anim)
    {
        direction = 1f;
        
        anim.SetBool("Down", false);
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
        anim.SetBool("Up", true);
    }
}

public class MoveDown : Command
{
    public override void Execute(out float direction, Animator anim)
    {
        direction = -1f;
        
        anim.SetBool("Right", false);
        anim.SetBool("Left",  false);
        anim.SetBool("Up",    false);
        anim.SetBool("Down",   true);
    }
}

public class MoveRight : Command
{
    public override void Execute(out float direction, Animator anim)
    {
        direction = 1f;
        
        anim.SetBool("Down", false);
        anim.SetBool("Left", false);
        anim.SetBool("Up", false);
        anim.SetBool("Right", true);
    }
}

public class MoveLeft : Command
{
    public override void Execute(out float direction, Animator anim)
    {
        direction = -1f;
        
        anim.SetBool("Down", false);
        anim.SetBool("Right", false);
        anim.SetBool("Up", false);
        anim.SetBool("Left", true);
    }
}

#endregion

public class DoNothing : Command
{
    public override void Execute(out float direction, Animator anim)
    {
        direction = 0f;
    }
}