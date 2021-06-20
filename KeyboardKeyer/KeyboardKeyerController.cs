namespace KeyboardKeyer
{
    using System;
    using HMUI;
    using IPA.Utilities;
    using UnityEngine;

    internal class KeyboardKeyerController : MonoBehaviour
    {
        private static readonly FieldAccessor<UIKeyboard, Action>.Accessor _deleteButtonWasPressedEventAccessor = FieldAccessor<UIKeyboard, Action>.GetAccessor("deleteButtonWasPressedEvent");
        private static readonly FieldAccessor<UIKeyboard, Action>.Accessor _okButtonWasPressedEventAccessor = FieldAccessor<UIKeyboard, Action>.GetAccessor("okButtonWasPressedEvent");

        private UIKeyboard _keyboard;

        internal void Init(UIKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        private void OnGUI()
        {
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyDown)
            {
                KeyCode keyCode = e.keyCode;
                if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
                {
                    // convert top row keyboard numbers to numpad numbers
                    keyCode += 208;
                }

                switch (keyCode)
                {
                    case KeyCode.Backspace:
                        MulticastDelegate deleteDelegate = _deleteButtonWasPressedEventAccessor(ref _keyboard);
                        deleteDelegate?.DynamicInvoke();
                        break;

                    case KeyCode.Return:
                        MulticastDelegate okDelegate = _okButtonWasPressedEventAccessor(ref _keyboard);
                        okDelegate?.DynamicInvoke();
                        break;

                    default:
                        _keyboard.HandleKeyPress(keyCode);
                        break;
                }
            }
        }
    }
}
