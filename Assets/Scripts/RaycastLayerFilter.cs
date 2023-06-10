using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RaycastLayerFilter : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;

    private XRBaseInteractable _currentInteractable;

    private void Update()
    {
        if (TryGetCurrentInteractable(out _currentInteractable))
        {
            if (!IsOnLayer(_currentInteractable.gameObject, _layerMask))
            {
                _currentInteractable = null;
            }
        }
    }

    private bool TryGetCurrentInteractable(out XRBaseInteractable interactable)
    {
        interactable = null;
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {
            interactable = hitInfo.collider.gameObject.GetComponent<XRBaseInteractable>();
        }
        return interactable != null;
    }

    private bool IsOnLayer(GameObject gameObject, LayerMask layerMask)
    {
        return (layerMask & (1 << gameObject.layer)) != 0;
    }

    public void OnSelectEnter(XRBaseInteractor interactor)
    {
        if (_currentInteractable != null)
        {
            interactor.attachTransform.position = _currentInteractable.transform.position;
            interactor.attachTransform.rotation = _currentInteractable.transform.rotation;
            interactor.attachTransform.SetParent(_currentInteractable.transform, true);
        }
    }

    public void OnSelectExit(XRBaseInteractor interactor)
    {
        if (_currentInteractable != null && interactor.selectTarget == _currentInteractable)
        {
            interactor.attachTransform.SetParent(null, true);
            _currentInteractable = null;
        }
    }
}