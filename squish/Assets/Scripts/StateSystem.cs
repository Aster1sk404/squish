using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class StateSystem : MonoBehaviour
	{
		public CharacterController2D cc;
		public enum State { GroundState, AirState, FallState, RWallState, LWallState};
		public State state = State.AirState;

		public StateSystem(CharacterController2D controller) {
        cc = controller;
    	}

		public void EnterState()				// E N T E R - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		{
			switch (state) {
				case State.GroundState:
					// GROUND STATE ENTER()
					break;

				case State.AirState:
					// AIR STATE ENTER()
					break;

				case State.FallState:
					// FALL STATE ENTER()
					break;

				case State.RWallState:
					// RIGHT WALL STATE ENTER()
					break;

				case State.LWallState:
					// LEFT WALL STATE ENTER()
					break;

				default:
					Debug.Log("No states are active: Setting GroundState as the default state");
					state = State.GroundState;
					break; 
			}
		}

		public void DoState()					// D O - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		{
			switch (state) {
				case State.GroundState:
					// GROUND STATE DO()
					break;

				case State.AirState:
					// AIR STATE DO()
					break;

				case State.FallState:
					// FALL STATE DO()
					break;

				case State.RWallState:
					// RIGHT WALL STATE DO()
					break;

				case State.LWallState:
					// LEFT WALL STATE DO()
					break;

				default:
					Debug.Log("No states are active: Setting GroundState as the default state");
					state = State.GroundState;
					break;
			}
		}

		public void FixedDoState()				// F I X E D  D O - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		{
			switch (state) {
				case State.GroundState:
					// GROUND STATE FIXEDDO()
					if (cc.horMove != 0)
        			    cc.Move();
        			else
        			    cc.rb.linearVelocityX = 0;
        			if (cc.jumpClicked)
        			    cc.Jump();
					cc.rb.gravityScale = cc.defGravity;

        			if (!cc.IsGrounded()) // -> AIR STATE
        			    state = State.AirState;
					break;

				case State.AirState:
					// AIR STATE FIXEDDO()
					if (cc.horMove != 0)
            			cc.Move();
        			else
            			cc.rb.linearVelocityX = 0;
					cc.rb.gravityScale = cc.defGravity;

        			if (cc.rb.linearVelocityY < 0 && (!cc.IsLWalled() || !cc.IsRWalled())) // -> FALL STATE
            			state = State.FallState;
					else if (cc.IsGrounded()) // -> GROUND STATE
						state = State.AirState;
					else if (cc.IsLWalled()) // -> LEFT WALL STATE
					{
						if (cc.rb.linearVelocityY < 0)
							cc.rb.linearVelocityY = 0;
						state = State.LWallState;
					}
						
					else if (cc.IsRWalled())// -> RIGHT WALL STATE
					{
						if (cc.rb.linearVelocityY < 0)
							cc.rb.linearVelocityY = 0;
						state = State.RWallState;
					} 
						
					break;
					
				case State.FallState:
					// FALL STATE FIXEDDO()
					if (cc.horMove != 0)
            			cc.Move();
        			else
        			    cc.rb.linearVelocityX = 0;
					cc.rb.gravityScale = cc.fallGravity;

        			if (cc.IsGrounded()) // -> GROUND STATE
        			    state = State.GroundState;
					else if (cc.IsLWalled()) // -> LEFT WALL STATE
					{
						if (cc.rb.linearVelocityY < 0)
							cc.rb.linearVelocityY = 0;
						state = State.LWallState;
					}
						
					else if (cc.IsRWalled())// -> RIGHT WALL STATE
					{
						if (cc.rb.linearVelocityY < 0)
							cc.rb.linearVelocityY = 0;
						state = State.RWallState;
					} 
						
					break;

				case State.RWallState:
					// RIGHT WALL STATE FIXEDDO()
					if (cc.horMove < 0)
			            cc.Move();
			        else
			            cc.rb.linearVelocityX = 0;
						cc.rb.gravityScale = cc.slideGravity;
			        if (cc.jumpClicked)
			            cc.Jump(-1);

			        if (!cc.IsRWalled() && cc.IsGrounded()) // -> GROUND STATE
			            state = State.GroundState;
					else if (!cc.IsRWalled() && !cc.IsGrounded()) // -> AIR STATE
						state = State.AirState;
					break;

				case State.LWallState:
					// LEFT WALL STATE FIXEDDO()
					if (cc.horMove > 0)
			            cc.Move();
			        else
			            cc.rb.linearVelocityX = 0;
						cc.rb.gravityScale = cc.slideGravity;
			        if (cc.jumpClicked)
			            cc.Jump(1);

			        if (!cc.IsLWalled() && cc.IsGrounded()) // -> GROUND STATE
			            state = State.GroundState;
					else if (!cc.IsLWalled() && !cc.IsGrounded()) // -> AIR STATE
						state = State.AirState;
					break;

				default:
					Debug.Log("No states are active: Setting GroundState as the default state");
					state = State.GroundState;
					break;
			}
		}

		public void ExitState()					// E X I T - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
		{
			switch (state) {
				case State.GroundState:
					// GROUND STATE EXIT()
					break;

				case State.AirState:
					// AIR STATE EXIT()
					break;

				case State.FallState:
					// FALL STATE EXIT()
					break;

				case State.RWallState:
					// RIGHT WALL STATE EXIT()
					break;

				case State.LWallState:
					// LEFT WALL STATE EXIT()
					break;

				default:
					Debug.Log("No states are active: Setting GroundState as the default state");
					state = State.GroundState;
					break;
			}
		}
	}