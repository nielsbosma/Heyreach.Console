using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(config =>
{
    config.SetApplicationName("heyreach");
    config.SetApplicationVersion("1.0.0");

    config.AddBranch("auth", auth =>
    {
        auth.SetDescription("Authentication commands");
        auth.AddCommand<Heyreach.Console.Commands.Auth.CheckCommand>("check");
    });

    config.AddBranch("campaigns", campaigns =>
    {
        campaigns.SetDescription("Campaign management");
        campaigns.AddCommand<Heyreach.Console.Commands.Campaigns.ListCommand>("list");
        campaigns.AddCommand<Heyreach.Console.Commands.Campaigns.GetCommand>("get");
        campaigns.AddCommand<Heyreach.Console.Commands.Campaigns.PauseCommand>("pause");
        campaigns.AddCommand<Heyreach.Console.Commands.Campaigns.ResumeCommand>("resume");
        campaigns.AddCommand<Heyreach.Console.Commands.Campaigns.AddLeadsCommand>("add-leads");
        campaigns.AddCommand<Heyreach.Console.Commands.Campaigns.StopLeadCommand>("stop-lead");
    });

    config.AddBranch("lists", lists =>
    {
        lists.SetDescription("Lead list management");
        lists.AddCommand<Heyreach.Console.Commands.Lists.ListCommand>("list");
        lists.AddCommand<Heyreach.Console.Commands.Lists.GetCommand>("get");
        lists.AddCommand<Heyreach.Console.Commands.Lists.CreateCommand>("create");
        lists.AddCommand<Heyreach.Console.Commands.Lists.LeadsCommand>("leads");
        lists.AddCommand<Heyreach.Console.Commands.Lists.AddLeadCommand>("add-lead");
        lists.AddCommand<Heyreach.Console.Commands.Lists.RemoveLeadCommand>("remove-lead");
        lists.AddCommand<Heyreach.Console.Commands.Lists.CompaniesCommand>("companies");
    });

    config.AddBranch("leads", leads =>
    {
        leads.SetDescription("Lead operations");
        leads.AddCommand<Heyreach.Console.Commands.Leads.GetCommand>("get");
        leads.AddCommand<Heyreach.Console.Commands.Leads.ListsCommand>("lists");
        leads.AddCommand<Heyreach.Console.Commands.Leads.UpdateStatusCommand>("update-status");
    });

    config.AddBranch("senders", senders =>
    {
        senders.SetDescription("LinkedIn sender account management");
        senders.AddCommand<Heyreach.Console.Commands.Senders.ListCommand>("list");
        senders.AddCommand<Heyreach.Console.Commands.Senders.GetCommand>("get");
        senders.AddCommand<Heyreach.Console.Commands.Senders.NetworkCommand>("network");
    });

    config.AddBranch("conversations", conversations =>
    {
        conversations.SetDescription("Conversation management");
        conversations.AddCommand<Heyreach.Console.Commands.Conversations.ListCommand>("list");
        conversations.AddCommand<Heyreach.Console.Commands.Conversations.SendCommand>("send");
    });

    config.AddBranch("stats", stats =>
    {
        stats.SetDescription("Analytics and statistics");
        stats.AddCommand<Heyreach.Console.Commands.Stats.GetCommand>("get");
    });

    config.AddBranch("webhooks", webhooks =>
    {
        webhooks.SetDescription("Webhook management");
        webhooks.AddCommand<Heyreach.Console.Commands.Webhooks.ListCommand>("list");
        webhooks.AddCommand<Heyreach.Console.Commands.Webhooks.GetCommand>("get");
        webhooks.AddCommand<Heyreach.Console.Commands.Webhooks.CreateCommand>("create");
        webhooks.AddCommand<Heyreach.Console.Commands.Webhooks.UpdateCommand>("update");
        webhooks.AddCommand<Heyreach.Console.Commands.Webhooks.DeleteCommand>("delete");
    });

    config.AddBranch("tags", tags =>
    {
        tags.SetDescription("Tag management");
        tags.AddCommand<Heyreach.Console.Commands.Tags.CreateCommand>("create");
    });
});

return app.Run(args);
