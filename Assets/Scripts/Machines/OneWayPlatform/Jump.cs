﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.TextCore.Text;

////Handles all Jump logic for Character
//public class Jump : Character
//{
//    //Total number of jumps allowed
//    [SerializeField]
//    private int maxJumps = 3;
//    //How high the player should go when jump button is pressed
//    [SerializeField]
//    private float jumpForce = 100;
//    //How long the jump button should be held to perform maximum jump height
//    [SerializeField]
//    private float maxButtonHoldTime = .4f;
//    //How much additional air the player receives when holding the jump button
//    [SerializeField]
//    private float holdForce = 100;
//    //How close the player needs to be to a platform before considered grounded
//    [SerializeField]
//    private float distanceToCollider = .08f;
//    //How fast the player can rise while jumping; this prevents multiple jumps in succession from increasing the vertical velocity too much
//    [SerializeField]
//    private float maxJumpSpeed = 6;
//    //How fast the player can fall; this prevents the vertical velocity from decreasing the longer the player falls
//    [SerializeField]
//    private float maxFallSpeed = -20;
//    [SerializeField]
//    private float downwardJumpingFallSpeed = -30;
//    //How fast the player's vertical velocity needs to be before considered falling
//    [SerializeField]
//    private float fallSpeed = 3;
//    //How much the gravity should be changed for certain things
//    [SerializeField]
//    private float gravityMultipler = 4;
//    //What objects the player should react to as ground
//    [SerializeField]
//    private LayerMask collisionLayer;

//    //Checks to see if input for the jump is pressed
//    private bool jumpPressed;
//    //Checks to see if the input for jump is held down
//    private bool jumpHeld;
//    //How long the jump button has been held
//    private float buttonHoldTime;
//    //The original gravity value that gravityMultiplier should reset to
//    private float originalGravity;
//    //The number of jumps the player can perform after the initial jump
//    private int numberOfJumpsLeft;
//    //Very brief delay so OnCollisionStay2D method can still detect input
//    private float inputDelay;

//    //A bool that checks to see if the player is currently passing through a one way platform
//    [HideInInspector]
//    public bool passingThroughPlatform;
//    //Checks to see if the Input detects both a downward direction being held down and the jumpHeld button is true at the same time
//    [HideInInspector]
//    public bool downJumpPressed;
//    //Makes the player propel downwards faster than it should be allowed while falling
//    [HideInInspector]
//    public bool downwardJumping;

//    //Grabs the Collider2D of whatever GameObject the player is passing through as a one way platform
//    private Collider2D nextPlatform;

//    //Start method called in the Character script
//    protected override void Initializtion()
//    {
//        base.Initializtion();
//        //Sets up the buttonHoldTime to the original max value
//        buttonHoldTime = maxButtonHoldTime;
//        //Sets the original gravity to whatever gravity settings are setup on the Rigidbody2D
//        originalGravity = rb.gravityScale;
//        //Sets the total number of jumps left to the max value
//        numberOfJumpsLeft = maxJumps;
//    }

//    private void Update()
//    {
//        //Checks to see what buttons are pressed specifically for jump functions
//        CheckForInput();
//        //Checks to make sure the conditions for a jump are true to allow the FixedUpdate method to calculate jump speeds
//        CheckForJump();
//        //Checks if the player is grounded
//        GroundCheck();
//        //Turns collisions back on for one way platforms
//        TurnOnCollision();
//    }

//    private void FixedUpdate()
//    {
//        //If jump is allowed, calculates the values for velocity on the Rigidbody2D to perform a precise jump
//        IsJumping();
//    }

//    private void CheckForInput()
//    {
//        //Checks if the jump button is pressed
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            jumpPressed = true;
//        }
//        else
//            jumpPressed = false;
//        //Checks if the jump button is held down
//        if (Input.GetKey(KeyCode.Space))
//        {
//            jumpHeld = true;
//        }
//        else
//            jumpHeld = false;
//        //Checks to see if pressing down while also pressing the jump button
//        if (Input.GetAxis("Vertical") < 0 && jumpPressed)
//        {
//            //Resets inputDelay back to 0
//            inputDelay = 0;
//            downJumpPressed = true;
//        }
//        else
//        {
//            //Checks to see if inputDelay is less than .05f
//            if (inputDelay < .02f)
//            {
//                //Adds the amount of time since last frame to the inputDelay value
//                inputDelay += Time.deltaTime;
//            }
//            //If inputDelay is greater than .05f
//            if (inputDelay >= .02f)
//            {
//                downJumpPressed = false;
//            }
//        }
//        //Checks to see if the player is currently not in a grounded state while downJumpPressed
//        if (!character.isGrounded && Input.GetAxis("Vertical") < 0 && jumpPressed)
//        {
//            //Performs a downward jump to allow the player to fall faster
//            downwardJumping = true;
//            //Handles the logic to propel the player down
//            DownwardJump();
//            //Makes sure the player is currently passing through a platform
//            if (!passingThroughPlatform)
//            {
//                //Checks to see if there is a one way platform beneath the player so the player can automatically pass through it instead of colliding with it
//                CheckForPlatformBelow();
//            }
//        }
//    }

//    private void CheckForJump()
//    {
//        //Checks if the jump button is pressed and not pressing down
//        if (!downJumpPressed && !downwardJumping && jumpPressed)
//        {
//            //If the character is not grounded and hasn't performed an initial jump than this is likely because the player stepped off a ledge
//            if ((!character.isGrounded) && numberOfJumpsLeft == maxJumps)
//            {
//                //Doesn't allow the jump and returns out of method
//                character.isJumping = false;
//                return;
//            }
//            //Negates numberOfJumpsLeft by 1
//            numberOfJumpsLeft--;
//            //If the number of jumps left is not currently negative
//            if (numberOfJumpsLeft >= 0)
//            {
//                //Gives each jump fresh gravity values so each jump will perform the same
//                rb.gravityScale = originalGravity;
//                //Resets velocity for fresh jump
//                rb.velocity = new Vector2(rb.velocity.x, 0);
//                //Resets buttonHoldTime back to max value for a fresh jump
//                buttonHoldTime = maxButtonHoldTime;
//                //Sets isJumping bool found on Character script to true to enter the jumping state
//                character.isJumping = true;
//            }
//        }
//    }

//    //Handles Rigidbody2D calculations for the jump
//    private void IsJumping()
//    {
//        //Checks if character is in jump state
//        if (character.isJumping)
//        {
//            //Applies initial jump force
//            rb.AddForce(Vector2.up * jumpForce);
//            //Checks for additional air if holding down jump button
//            AdditionalAir();
//        }
//        //Limits jump vertical velocity so multiple jumps performed quickly don't propel the player upwards
//        if (rb.velocity.y > maxJumpSpeed)
//        {
//            //Sets the vertical velocity to the jump speed limit
//            rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed);
//        }
//        //Handles fall logic
//        Falling();
//    }

//    //Additional jump force based on holding down the jump button
//    private void AdditionalAir()
//    {
//        //Checks if jump button is held down
//        if (jumpHeld)
//        {
//            //Negates the buttonHoldTime value by time
//            buttonHoldTime -= Time.deltaTime;
//            //If the buttonHoldTime is 0 or less than 0
//            if (buttonHoldTime <= 0)
//            {
//                //Sets the buttonHoldTime to 0
//                buttonHoldTime = 0;
//                //Gets character out of jumping state
//                character.isJumping = false;
//            }
//            //If buttonHoldTime is greater than 0
//            else
//                //Performs additional jump height by the holdForce value
//                rb.AddForce(Vector2.up * holdForce);
//        }
//        //If not holding down the jump button any longer and buttonHOldTime is greater than 0
//        else
//        {
//            //Gets character out of jumping state
//            character.isJumping = false;
//        }
//    }

//    //Jump that pushes player downwards for quicker falling
//    private void DownwardJump()
//    {
//        if (downwardJumping)
//        {
//            //Pushes player down instead of up for a downward jump
//            rb.velocity = new Vector2(rb.velocity.x, downwardJumpingFallSpeed);
//        }
//    }

//    //Manages fall speeds and if the character is in the falling state
//    private void Falling()
//    {
//        if (!downwardJumping)
//        {
//            //If character is not currently jumping up and the vertical velocity is officially in the falling state
//            if (!character.isJumping && rb.velocity.y < fallSpeed)
//            {
//                //Pushes the player down a bit faster to perform a more specific jump often found in platformers and not have such a floaty jump
//                rb.gravityScale = gravityMultipler;
//            }
//            //If the vertical velocity is less than the fastest the player should be falling
//            if (rb.velocity.y < maxFallSpeed)
//            {
//                //Sets vertical velocity to the maximum speed allowed to fall
//                rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
//            }
//        }
//    }

//    //Checks if the player is currently grounded or not
//    private void GroundCheck()
//    {
//        //Method found in the Character script that checks if the player is touching a ground platform and if the character is not in a jumping state
//        if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !character.isJumping)
//        {
//            //Checks to see if either the passingThroughPlatform bool is true or if the nextPlatform Collider2D is not null
//            if (passingThroughPlatform || nextPlatform != null)
//            {
//                //Sets a parameter on Animator component to allow jump and falling based animations to play
//                anim.SetBool("Grounded", false);
//                //Player is not longer in grounded state
//                character.isGrounded = false;
//            }
//            //If CollisionCheck() returns true and the passingThroughPlatform bool is false and nextPlatform Collider2D is null
//            else
//            {
//                //Player enters a grounded state
//                character.isGrounded = true;
//                //Sets the Animator to the Grounded state
//                anim.SetBool("Grounded", true);
//                //Resest the numberOfJumpsLeft back to max value
//                numberOfJumpsLeft = maxJumps;
//                //Resets gravity back to original value
//                rb.gravityScale = originalGravity;
//                //Turns off the downwardJumping bool so the player isn't being forced down fast anymore
//                downwardJumping = false;
//            }
//        }
//        //If the above if statement returns false, then character is not touching platform or is in a jumping state
//        else
//        {
//            //Sets a parameter on the Animator component to feed the yVelocity value the current Rigidbody2D velocity y value
//            anim.SetFloat("yVelocity", rb.velocity.y);
//            //Sets a parameter on Animator component to allow jump and falling based animations to play
//            anim.SetBool("Grounded", false);
//            //Player is not longer in grounded state
//            character.isGrounded = false;
//        }
//    }

//    private void CheckForPlatformBelow()
//    {
//        //Performs a raycast to see if a platform layer is beneath the player
//        RaycastHit2D hit = Physics2D.Raycast(new Vector2(col.bounds.center.x, col.bounds.min.y), Vector2.down, Mathf.Infinity, collisionLayer);
//        //Checks to see if the colliding platform beneath the player is a one way platform and allows the player to pass through it based on the one way platform type
//        if (hit.collider.GetComponent<OneWayPlatform>()
//            && (hit.collider.GetComponent<OneWayPlatform>().type != OneWayPlatform.OneWayPlatforms.GoingUp))
//        {
//            //Sets the private gameobject passedThroughPlatform to the current raycast hit platform
//            nextPlatform = hit.collider;
//            //Ignores the current platform that the player should pass through because the player is downward jumping from above the platform
//            Physics2D.IgnoreCollision(col, nextPlatform, true);
//        }
//    }

//    private void TurnOnCollision()
//    {
//        if (nextPlatform != null && col.bounds.max.y < nextPlatform.bounds.min.y)
//        {
//            //Turns on the collision for the player and the passedThroughPlatform
//            Physics2D.IgnoreCollision(col, nextPlatform, false);
//            //Sets the passedThroughPlatform back to null
//            nextPlatform = null;
//        }
//    }
//}