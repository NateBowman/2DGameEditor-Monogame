﻿#region Usings

using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharedGameData.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

#endregion

namespace SharedGameData {
    public static class StaticGlobalInput {
        public static KeyboardState currentKeys;
        public static MouseState currentMouse;
        public static KeyboardState previousKeys;
        public static MouseState previousMouse;

        private static bool inputHandlersReady;
        public static NativeKeyboardInput KeyboardInputHandler { get; set; }
        public static MGForms_MouseInput MouseInputHandler { get; set; }

        public static void HandleMouseButtons(MouseEventArgs e, bool buttonPressed) {
            MouseInputHandler.HandleMouseButtons(e, buttonPressed);
            UpdateInputStates();
        }

        public static void HandleMouseMovement(MouseEventArgs e) {
            MouseInputHandler.HandleMouseMove(e);
            UpdateInputStates();
        }

        public static void HandleMouseWheel(MouseEventArgs e) {
            MouseInputHandler.HandleMouseWheel(e);
            UpdateInputStates();
        }

        public static void InitialiseInputHandlers(Control mouseFocalControl) {
            KeyboardInputHandler = new NativeKeyboardInput();
            MouseInputHandler = new MGForms_MouseInput(mouseFocalControl);
            Application.Idle += delegate { UpdateInputStates(); };
            inputHandlersReady = true;
        }

        public static bool IsNewKeyPress(Keys key) {
            return previousKeys.IsKeyUp(key) && currentKeys.IsKeyDown(key);
        }

        public static bool IsNewLeftClick() {
            return (previousMouse.LeftButton == ButtonState.Released) && (currentMouse.LeftButton == ButtonState.Pressed);
        }

        public static bool IsNewMiddleClick() {
            return (previousMouse.LeftButton == ButtonState.Released) && (currentMouse.LeftButton == ButtonState.Pressed);
        }

        public static bool IsNewRightClick() {
            return (previousMouse.LeftButton == ButtonState.Released) && (currentMouse.LeftButton == ButtonState.Pressed);
        }

        public static void UpdateInputStates() {
            if (!inputHandlersReady) {
                return;
            }

            previousKeys = currentKeys;
            previousMouse = currentMouse;

            currentKeys = KeyboardInputHandler.GetState();
            currentMouse = MouseInputHandler.MouseState;
        }

        public static Point GetMousePosDelta() {
            return currentMouse.Position - previousMouse.Position;
        }
    }
}