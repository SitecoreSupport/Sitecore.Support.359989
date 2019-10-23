namespace Sitecore.Support
{
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.SecurityModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class WorkflowPanelHook : Sitecore.Events.Hooks.IHook
    {
        private string _coreDatabaseName;
        private string _workflowPanelItemID;
        public WorkflowPanelHook(string coreDatabaseName, string workflowPanelItemID)
        {
            this._coreDatabaseName = coreDatabaseName;
            this._workflowPanelItemID = workflowPanelItemID;
        }

        public void Initialize()
        {
            Database database = Sitecore.Configuration.Factory.GetDatabase(_coreDatabaseName);

            using (new SecurityDisabler())
            {
                Item workflowPanel = database.GetItem(new ID(_workflowPanelItemID));

                workflowPanel.Editing.BeginEdit();
                workflowPanel.Fields["Type"].Value = "Sitecore.Support.Shell.Applications.ContentManager.Panels.WorkflowPanel,Sitecore.Support.359989";
                workflowPanel.Editing.EndEdit();
            }
        }
    }
}