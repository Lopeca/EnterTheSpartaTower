using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    bool IsInteracting { get; set; }

    void Interact();
} 