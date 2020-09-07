﻿using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRSocketInteractor
{
    [Tooltip("Arrow to instantiate")]
    public GameObject arrowPrefab;

    //Position to which the arrow is attached.
    private Vector3 attachOffset = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        CreateAndSelectArrow();
        SetAttachOffset();
    }

    protected override void OnSelectExit(XRBaseInteractable interactable)
    {
        base.OnSelectExit(interactable);
        CreateAndSelectArrow();
    }

    private void CreateAndSelectArrow()
    {
        Arrow arrow = CreateArrow();
        SelectArrow(arrow);
    }

    private Arrow CreateArrow()
    {
        GameObject arrowObject = Instantiate(arrowPrefab, transform.position - attachOffset, transform.rotation);
        return arrowObject.GetComponent<Arrow>();
    }

    private void SelectArrow(Arrow arrow)
    {
        OnSelectEnter(arrow);
        arrow.OnSelectEnter(this);
    }

    private void SetAttachOffset()
    {
        if (selectTarget is XRGrabInteractable interactable)
        {
            attachOffset = interactable.attachTransform.localPosition;
        }
    }
}
