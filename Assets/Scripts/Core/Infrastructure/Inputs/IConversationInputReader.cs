using System;
using UnityEngine;

namespace Infrastructure.Core.Inputs
{
    public interface IConversationInputReader
    {
        public event Action OnCompleteDialogPressed;
        public event Action OnSelectDialogPressed;
        public event Action<Vector2> OnNavigatePressed;
    }
}