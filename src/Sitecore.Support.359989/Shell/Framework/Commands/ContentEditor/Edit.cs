namespace Sitecore.Support.Shell.Framework.Commands.ContentEditor
{
    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Shell.Framework.Commands;
    
    public class Edit : Sitecore.Shell.Framework.Commands.ContentEditor.Edit
    {
        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");

            if (context.Items.Length != 1)
            {
                return CommandState.Hidden;
            }
            Item item = context.Items[0];
            if (item.IsFallback)
            {
                return CommandState.Disabled;
            }
            return ((!base.HasField(item, FieldIDs.Workflow) || !base.HasField(item, FieldIDs.WorkflowState)) ? CommandState.Hidden : (HasWriteAccess(item) ? (item.Access.CanWriteLanguage() ? (!CanCheckIn(item) ? (!CanEdit(item) ? CommandState.Disabled : CommandState.Enabled) : CommandState.Down) : CommandState.Disabled) : CommandState.Disabled));
        }


    }
}