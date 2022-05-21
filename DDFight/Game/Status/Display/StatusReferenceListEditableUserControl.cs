using BaseToolsLibrary.DependencyInjection;
using DDFight.Controlers;
using DnDToolsLibrary.Status;
using System;
using DDFight.WpfExtensions;

namespace DDFight.Game.Status.Display
{
    public class StatusReferenceListEditableUserControl : SpecializedListUserControl<StatusReference>
    {
        private static Lazy<IStatusProvider> _lazyStatusProvider = new (DIContainer.GetImplementation<IStatusProvider>());
        private static IStatusProvider _statusProvider => _lazyStatusProvider.Value;

        public StatusReferenceListEditableUserControl()
        {
            DataContextChanged += StatusReferenceListEditableUserControl_DataContextChanged;
        }

        private StatusReferenceList _dataContext => DataContext as StatusReferenceList;

        private void StatusReferenceListEditableUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            this.EntityList = _dataContext;
        }

        public override bool edit(object element)
        {
            var statusRef = element as StatusReference;
            
            // means its a ref that is not related to a OnHitStatus in the StatusProvider 
            // => either a fully custom status, or a status that remains long after a fight
            if (statusRef.ActualStatusReferenceId == default)
            {
                return statusRef.OpenEditWindow();
            }
            // means its a ref for something we can actually find in the StatusProvider
            else
            {
                var status = _statusProvider.GetOnHitStatusById(statusRef.ActualStatusReferenceId);
                return status.OpenEditWindow();
            }
        }
    }
}
