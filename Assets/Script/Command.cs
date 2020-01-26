using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void ExecuteMovement(Rigidbody2D rb2D, float speed);
}

public class MoveUp : Command
{
    public override void ExecuteMovement(Rigidbody2D rb2D, float speed)
    {
        rb2D.velocity += Vector2.up * Time.deltaTime * speed;
    }
}

public class MoveDown : Command
{
    public override void ExecuteMovement(Rigidbody2D rb2D, float speed)
    {
        rb2D.velocity += Vector2.down * Time.deltaTime * speed;
    }
}

public class MoveLeft : Command
{
    public override void ExecuteMovement(Rigidbody2D rb2D, float speed)
    {
        rb2D.velocity += Vector2.left * Time.deltaTime * speed;
    }
}

public class MoveRight : Command
{
    public override void ExecuteMovement(Rigidbody2D rb2D, float speed)
    {
        rb2D.velocity += Vector2.right * Time.deltaTime * speed;
    }
}

public class DoNothing : Command
{
    public override void ExecuteMovement(Rigidbody2D rb2D, float speed)
    {
    }
}