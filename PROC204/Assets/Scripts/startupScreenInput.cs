// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/startupScreenInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @StartupScreenInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @StartupScreenInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""startupScreenInput"",
    ""maps"": [
        {
            ""name"": ""checkInput"",
            ""id"": ""1dc8a5f4-9f49-4f07-b617-7f8ed139519d"",
            ""actions"": [
                {
                    ""name"": ""playerA"",
                    ""type"": ""Button"",
                    ""id"": ""36be6894-52b4-4711-8652-6cdfb9db0a84"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""285f0ac9-5bd8-4416-b10d-b089469f2029"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""playerA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // checkInput
        m_checkInput = asset.FindActionMap("checkInput", throwIfNotFound: true);
        m_checkInput_playerA = m_checkInput.FindAction("playerA", throwIfNotFound: true);
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

    // checkInput
    private readonly InputActionMap m_checkInput;
    private ICheckInputActions m_CheckInputActionsCallbackInterface;
    private readonly InputAction m_checkInput_playerA;
    public struct CheckInputActions
    {
        private @StartupScreenInput m_Wrapper;
        public CheckInputActions(@StartupScreenInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @playerA => m_Wrapper.m_checkInput_playerA;
        public InputActionMap Get() { return m_Wrapper.m_checkInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheckInputActions set) { return set.Get(); }
        public void SetCallbacks(ICheckInputActions instance)
        {
            if (m_Wrapper.m_CheckInputActionsCallbackInterface != null)
            {
                @playerA.started -= m_Wrapper.m_CheckInputActionsCallbackInterface.OnPlayerA;
                @playerA.performed -= m_Wrapper.m_CheckInputActionsCallbackInterface.OnPlayerA;
                @playerA.canceled -= m_Wrapper.m_CheckInputActionsCallbackInterface.OnPlayerA;
            }
            m_Wrapper.m_CheckInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @playerA.started += instance.OnPlayerA;
                @playerA.performed += instance.OnPlayerA;
                @playerA.canceled += instance.OnPlayerA;
            }
        }
    }
    public CheckInputActions @checkInput => new CheckInputActions(this);
    public interface ICheckInputActions
    {
        void OnPlayerA(InputAction.CallbackContext context);
    }
}
