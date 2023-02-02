using System;
namespace OMicrosoft.Extensions.DependencyInjection
{
    //Normally this is where I would configure infrastructure dependencies and get them injected into the service provider.
    //Connectors would live in Application and then implementations would live in this in project.
    //Ex: TimeService, Repositories, HttpClients, etc...
    //Anything reaching outside of the application layer would be in here.
    //This project didn't call for it however.
    //I did think about pulling the KeyBoard dictionary from a json file just to have something here, but it didn't seem worth it. 
    public static class ConfigureInfrastructureServices
    {

    }
}

