// GENERATED AUTOMATICALLY FROM 'Assets/Art Import/UI/Icons/Input/Input Actions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Actions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""1119f143-03e2-4434-8b36-e2d21c8d33a6"",
            ""actions"": [
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""03628409-4788-43e9-8bd8-23d696fb5118"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""01d3160c-2f7d-43bb-b0a5-b57cfc3cfab9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Click"",
                    ""type"": ""Button"",
                    ""id"": ""562214d3-431f-4967-8d07-55e2c77ca582"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""7ad8d905-b0f8-4400-b101-443aad2c4c5b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Any Key"",
                    ""type"": ""Button"",
                    ""id"": ""91bf6541-4a0a-4727-86ca-543b5bdf5f85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b9956380-f592-4720-a551-b8757ed5ea65"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8265af98-91e2-43bb-8b04-0d1d8e062af5"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0effcbac-9794-4298-b327-4f1265e22eaf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37a64fb8-c92f-400e-8a1f-90c574ccb28d"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1203c65d-4c73-492e-a998-6a3536ced1e0"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any Key"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Mouse = m_Player.FindAction("Mouse", throwIfNotFound: true);
        m_Player_Escape = m_Player.FindAction("Escape", throwIfNotFound: true);
        m_Player_MouseClick = m_Player.FindAction("Mouse Click", throwIfNotFound: true);
        m_Player_Scroll = m_Player.FindAction("Scroll", throwIfNotFound: true);
        m_Player_AnyKey = m_Player.FindAction("Any Key", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Mouse;
    private readonly InputAction m_Player_Escape;
    private readonly InputAction m_Player_MouseClick;
    private readonly InputAction m_Player_Scroll;
    private readonly InputAction m_Player_AnyKey;
    public struct PlayerActions
    {
        private @InputActions m_Wrapper;
        public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Mouse => m_Wrapper.m_Player_Mouse;
        public InputAction @Escape => m_Wrapper.m_Player_Escape;
        public InputAction @MouseClick => m_Wrapper.m_Player_MouseClick;
        public InputAction @Scroll => m_Wrapper.m_Player_Scroll;
        public InputAction @AnyKey => m_Wrapper.m_Player_AnyKey;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Mouse.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouse;
                @Escape.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEscape;
                @MouseClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @Scroll.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScroll;
                @AnyKey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAnyKey;
                @AnyKey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAnyKey;
                @AnyKey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAnyKey;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
                @AnyKey.started += instance.OnAnyKey;
                @AnyKey.performed += instance.OnAnyKey;
                @AnyKey.canceled += instance.OnAnyKey;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMouse(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
        void OnAnyKey(InputAction.CallbackContext context);
    }
}
