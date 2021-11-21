// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace TowerDeffence
{
    public class @PlayerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""MouseInteractions"",
            ""id"": ""d4bcb836-25d1-462b-9a5d-7c0e295ca8bd"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""1fb6b2c6-f7ad-4088-950d-37d1448a196a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""a2658aa5-3f4c-4e35-bf5d-52f50b78f164"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1ac1d8a5-ad6a-4640-aa20-704ac275754d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8ca13f6-8022-4d5f-9920-66febf6ea419"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""KeyboardControls"",
            ""id"": ""98ea87c3-b797-479e-a737-9a04d0550c9f"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""b86e54e3-6323-4395-a9ad-f554c3a954a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""577c4242-2f05-4f52-94e4-63bb742cc5f7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // MouseInteractions
            m_MouseInteractions = asset.FindActionMap("MouseInteractions", throwIfNotFound: true);
            m_MouseInteractions_Click = m_MouseInteractions.FindAction("Click", throwIfNotFound: true);
            m_MouseInteractions_MousePosition = m_MouseInteractions.FindAction("MousePosition", throwIfNotFound: true);
            // KeyboardControls
            m_KeyboardControls = asset.FindActionMap("KeyboardControls", throwIfNotFound: true);
            m_KeyboardControls_Escape = m_KeyboardControls.FindAction("Escape", throwIfNotFound: true);
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

        // MouseInteractions
        private readonly InputActionMap m_MouseInteractions;
        private IMouseInteractionsActions m_MouseInteractionsActionsCallbackInterface;
        private readonly InputAction m_MouseInteractions_Click;
        private readonly InputAction m_MouseInteractions_MousePosition;
        public struct MouseInteractionsActions
        {
            private @PlayerControls m_Wrapper;
            public MouseInteractionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Click => m_Wrapper.m_MouseInteractions_Click;
            public InputAction @MousePosition => m_Wrapper.m_MouseInteractions_MousePosition;
            public InputActionMap Get() { return m_Wrapper.m_MouseInteractions; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MouseInteractionsActions set) { return set.Get(); }
            public void SetCallbacks(IMouseInteractionsActions instance)
            {
                if (m_Wrapper.m_MouseInteractionsActionsCallbackInterface != null)
                {
                    @Click.started -= m_Wrapper.m_MouseInteractionsActionsCallbackInterface.OnClick;
                    @Click.performed -= m_Wrapper.m_MouseInteractionsActionsCallbackInterface.OnClick;
                    @Click.canceled -= m_Wrapper.m_MouseInteractionsActionsCallbackInterface.OnClick;
                    @MousePosition.started -= m_Wrapper.m_MouseInteractionsActionsCallbackInterface.OnMousePosition;
                    @MousePosition.performed -= m_Wrapper.m_MouseInteractionsActionsCallbackInterface.OnMousePosition;
                    @MousePosition.canceled -= m_Wrapper.m_MouseInteractionsActionsCallbackInterface.OnMousePosition;
                }
                m_Wrapper.m_MouseInteractionsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Click.started += instance.OnClick;
                    @Click.performed += instance.OnClick;
                    @Click.canceled += instance.OnClick;
                    @MousePosition.started += instance.OnMousePosition;
                    @MousePosition.performed += instance.OnMousePosition;
                    @MousePosition.canceled += instance.OnMousePosition;
                }
            }
        }
        public MouseInteractionsActions @MouseInteractions => new MouseInteractionsActions(this);

        // KeyboardControls
        private readonly InputActionMap m_KeyboardControls;
        private IKeyboardControlsActions m_KeyboardControlsActionsCallbackInterface;
        private readonly InputAction m_KeyboardControls_Escape;
        public struct KeyboardControlsActions
        {
            private @PlayerControls m_Wrapper;
            public KeyboardControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Escape => m_Wrapper.m_KeyboardControls_Escape;
            public InputActionMap Get() { return m_Wrapper.m_KeyboardControls; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(KeyboardControlsActions set) { return set.Get(); }
            public void SetCallbacks(IKeyboardControlsActions instance)
            {
                if (m_Wrapper.m_KeyboardControlsActionsCallbackInterface != null)
                {
                    @Escape.started -= m_Wrapper.m_KeyboardControlsActionsCallbackInterface.OnEscape;
                    @Escape.performed -= m_Wrapper.m_KeyboardControlsActionsCallbackInterface.OnEscape;
                    @Escape.canceled -= m_Wrapper.m_KeyboardControlsActionsCallbackInterface.OnEscape;
                }
                m_Wrapper.m_KeyboardControlsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Escape.started += instance.OnEscape;
                    @Escape.performed += instance.OnEscape;
                    @Escape.canceled += instance.OnEscape;
                }
            }
        }
        public KeyboardControlsActions @KeyboardControls => new KeyboardControlsActions(this);
        public interface IMouseInteractionsActions
        {
            void OnClick(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
        }
        public interface IKeyboardControlsActions
        {
            void OnEscape(InputAction.CallbackContext context);
        }
    }
}
