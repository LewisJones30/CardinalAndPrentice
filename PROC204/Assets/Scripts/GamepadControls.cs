// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/GamepadControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GamepadControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GamepadControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GamepadControls"",
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
                    ""id"": ""f476f887-8a11-4e64-a2c5-4941003c9157"",
                    ""path"": ""<Gamepad>/dpad"",
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
            ""id"": ""b4eb2601-3b09-4006-9149-b034476ceb73"",
            ""actions"": [
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""73bac916-2628-4e98-9575-355d38d16cae"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""017c43e9-a383-4135-887c-a0eaef092857"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5042e0dc-9010-4101-a2a2-237189732816"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89740c4e-3f92-4d2e-8752-2af15294b314"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63ff0d85-1be1-4b97-a52a-b3b62a065fd6"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
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
        m_Prentice_Fire = m_Prentice.FindAction("Fire", throwIfNotFound: true);
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
        private @GamepadControls m_Wrapper;
        public CardinalActions(@GamepadControls wrapper) { m_Wrapper = wrapper; }
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
    private readonly InputAction m_Prentice_Fire;
    public struct PrenticeActions
    {
        private @GamepadControls m_Wrapper;
        public PrenticeActions(@GamepadControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Aim => m_Wrapper.m_Prentice_Aim;
        public InputAction @Fire => m_Wrapper.m_Prentice_Fire;
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
                @Fire.started -= m_Wrapper.m_PrenticeActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PrenticeActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PrenticeActionsCallbackInterface.OnFire;
            }
            m_Wrapper.m_PrenticeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
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
        void OnFire(InputAction.CallbackContext context);
    }
}
