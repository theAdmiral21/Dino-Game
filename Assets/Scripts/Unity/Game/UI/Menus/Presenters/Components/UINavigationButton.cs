using UnityEngine;
using System;
using Game.UI.Menus.Unity.Presenters.Pages.DataStructures;
using Primitives.UI.Menus.Unity.Presenters.Components;
using Game.UI.Menus.Unity.Presenters.Abstractions;

namespace Game.UI.Menus.Unity.Presenters.Components
{
    public class UINavigationButton : BaseSelectable
    {
        [SerializeField] private PageToken _token;

        public override Type PresenterType => typeof(UIButtonPresenter);


    }

}