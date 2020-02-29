// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/GamepadController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GamepadController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GamepadController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GamepadController"",
    ""maps"": [
        {
            ""name"": ""Cardinal"",
            ""id"": ""806dbdbe-b00c-4451-9ec7-a3342793463b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""6d717c37-baf8-4f1a-8bdd-ba8138f33552"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""0c637668-4c75-4cf1-9d9a-5c2af22a7af9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""040ca9d6-0dbb-4a59-a34d-8c257d69fa99"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""46d9238d-4656-4396-a87a-cba4519d70aa"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0438779-80bc-4956-994d-46ae5b8c2429"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f293b02-708d-47e0-ac84-330db7b56fb4"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Prentice"",
            ""id"": ""acb825f9-af3a-4d59-8223-cefe279bffbc"",
            ""actions"": [
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""a00bed91-5c60-4423-acd9-9fa47caf0eac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b9d0d6b-da19-40fe-a288-8bfa6c29b17c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Cardinal
        m_Cardinal = asset.FindActionMap("Cardinal", throwIfNotFound: true);
        m_Cardinal_Movement = m_Cardinal.FindAction("Movement", throwIfNotFound: true);
        m_Cardinal_Jump = m_Cardinal.FindAction("Jump", throwIfNotFound: true);
        m_Cardinal_Attack = m_Cardinal.FindAction("Attack", throwIfNotFound: true);
        // Prentice
        m_Prentice = asset.FindActionMap("Prentice", throwIfNotFound: true);
        m_Prentice_Aim = m_Prentice.FindAction("Aim", throwIfNotFound: true);
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

    // Cardinal
    private readonly InputActionMap m_Cardinal;
    private ICardinalActions m_CardinalActionsCallbackInterface;
    private readonly InputAction m_Cardinal_Movement;
    private readonly InputAction m_Cardinal_Jump;
    private readonly InputAction m_Cardinal_Attack;
    public struct CardinalActions
    {
        private @GamepadController m_Wrapper;
        public CardinalActions(@GamepadController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Cardinal_Movement;
        public InputAction @Jump => m_Wrapper.m_Cardinal_Jump;
        public InputAction @Attack => m_Wrapper.m_Cardinal_Attack;
        public InputActionMap Get() { return m_Wrapper.m_Cardinal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CardinalActions set) { return set.Get(); }
        public void SetCallbacks(ICardinalActions instance)
        {
            if (m_Wrapper.m_CardinalActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CardinalActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CardinalActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CardinalActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_CardinalActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CardinalActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CardinalActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_CardinalActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CardinalActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CardinalActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_CardinalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public CardinalActions @Cardinal => new CardinalActions(this);

    // Prentice
    private readonly InputActionMap m_Prentice;
    private IPrenticeActions m_PrenticeActionsCallbackInterface;
    private readonly InputAction m_Prentice_Aim;
    public struct PrenticeActions
    {
        private @GamepadController m_Wrapper;
        public PrenticeActions(@GamepadController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Aim => m_Wrapper.m_Prentice_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Prentice; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PrenticeActions set) { return set.Get(); }
        public void SetCallbacks(IPrenticeActions instance)
        {
            if (m_Wrapper.m_PrenticeActionsCallbackInterface != null)
            {
                @Aim.started -= m_Wrapper.m_PrenticeActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PrenticeActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PrenticeActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_PrenticeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public PrenticeActions @Prentice => new PrenticeActions(this);
    public interface ICardinalActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
    public interface IPrenticeActions
    {
        void OnAim(InputAction.CallbackContext context);
    }
}
