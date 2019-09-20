namespace Sitecore.Support.Shell.Framework.Commands
{
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;
    using System;
    using System.Collections.Specialized;
    public class CheckIn : Sitecore.Shell.Framework.Commands.CheckIn
    {
        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            if (context.Items.Length != 1)
            {
                return CommandState.Hidden;
            }
            Item item = context.Items[0];
            if (Context.IsAdministrator)
            {
                if (!item.Locking.IsLocked())
                {
                    return CommandState.Hidden;
                }
                if (item.IsFallback)
                {
                    return CommandState.Disabled;
                }
                return CommandState.Enabled;
            }
            if (item.Appearance.ReadOnly)
            {
                return CommandState.Disabled;
            }
            if (!item.Access.CanWrite())
            {
                return CommandState.Disabled;
            }
            if (!item.Locking.HasLock())
            {
                return CommandState.Disabled;
            }
            if (!item.Access.CanWriteLanguage())
            {
                return CommandState.Disabled;
            }
            return base.QueryState(context);
        }
    }
}