using System;
using Core.Environment.Objectives;
using Environment.Core.Interactions;
using Primitives.Environment.Platforms;
using UnityEngine;

namespace Unity.Environment.Objectives
{
    public class InteractObjective : MonoBehaviour, IObjective, IInteractable
    {
        public event Action<IObjective> OnComplete;
        private bool _isActive;

        public ObjectiveId Id => _id;
        [SerializeField] private ObjectiveId _id;
        public bool CanInteract()
        {
            return _isActive;
        }
        public void Interact()
        {
            if (!CanInteract()) return;

            TryComplete();
        }

        public void Activate()
        {
            _isActive = true;
        }
        public void Deactivate()
        {
            _isActive = false;
        }
        public void TryComplete()
        {
            OnComplete?.Invoke(this);
        }
    }
}