using System;
using IPA.Utilities;
using UnityEngine;

namespace KeyboardKeyer;

[RequireComponent(typeof(HMUI.UIKeyboard))]
internal class KeyboardKeyer : MonoBehaviour
{
    private static readonly FieldAccessor<HMUI.UIKeyboard, Action>.Accessor _deleteButtonWasPressedEventAccessor =
        FieldAccessor<HMUI.UIKeyboard, Action>.GetAccessor("deleteButtonWasPressedEvent");

    private static readonly FieldAccessor<HMUI.UIKeyboard, Action<char>>.Accessor _keyWasPressedEventAccessor =
        FieldAccessor<HMUI.UIKeyboard, Action<char>>.GetAccessor("keyWasPressedEvent");

    private static readonly FieldAccessor<HMUI.UIKeyboard, Action>.Accessor _okButtonWasPressedEventAccessor =
        FieldAccessor<HMUI.UIKeyboard, Action>.GetAccessor("okButtonWasPressedEvent");

    private bool _caps;

    private HMUI.UIKeyboard _keyboard = null!;

    private void Awake()
    {
        _keyboard = GetComponent<HMUI.UIKeyboard>();
    }

    private void OnGUI()
    {
        Event e = Event.current;

        if (!e.isKey)
        {
            return;
        }

        // e.capsLock documentation is incorrect
        ////bool caps = e.capsLock;
        bool caps = _caps;
        if (e.shift)
        {
            caps = !caps;
        }

        if (caps != _keyboard._shouldCapitalize)
        {
            _keyboard._shouldCapitalize = caps;
            _keyboard.SetKeyboardCapitalization(caps);
        }

        if (e.type != EventType.KeyDown)
        {
            return;
        }

        KeyCode keyCode = e.keyCode;
        if (keyCode != KeyCode.None)
        {
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

                case KeyCode.CapsLock:
                    _caps = !_caps;
                    break;
            }
        }
        else if (!char.IsControl(e.character))
        {
            MulticastDelegate keyDelegate = _keyWasPressedEventAccessor(ref _keyboard);
            keyDelegate?.DynamicInvoke(e.character);
        }
    }
}
