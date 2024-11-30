using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputMapperPlugin // Use a namespace to simulate modularity
{
    public static class InputMapper
    {
        public static Vector2 GetMovementInput()
        {
            // Returns movement input (supports keyboard and controller)
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            return new Vector2(horizontal, vertical);
        }

        public static bool IsRotatePressed()
        {
            // Checks if rotate button is pressed
            return Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1");
        }

        public static bool IsDropPressed()
        {
            // Checks if drop button is pressed
            return Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire2");
        }
    }
}
