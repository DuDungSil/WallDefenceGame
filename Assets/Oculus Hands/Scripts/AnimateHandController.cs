using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]
public class AnimateHandController : MonoBehaviour
{
    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;


    private XRDirectInteractor _interactor;
    private Animator _handAnimator;
    private float _gripValue;
    private float _triggerValue;

    private void Start()
    {
        _interactor = transform.parent.transform.parent.GetComponent<XRDirectInteractor>();
        _handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_interactor.hasSelection) setAnimateGrip(1);
        else AnimateGrip();
        AnimateTrigger();
    }
    private void AnimateGrip()
    {
        _gripValue = gripInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Grip", _gripValue);
    }
    public void setAnimateGrip(float value)
    {
        _handAnimator.SetFloat("Grip", value);
    }

    private void AnimateTrigger()
    {
        _triggerValue = triggerInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);
    }
}
