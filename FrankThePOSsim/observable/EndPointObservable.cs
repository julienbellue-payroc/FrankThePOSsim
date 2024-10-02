using System.Collections.ObjectModel;
using FrankThePOSsim.Models;

namespace FrankThePOSsim.observable;

internal class EndPointObservable : ObservableCollection<Endpoint>
{
    public EndPointObservable(Endpoints endpoints)
    {
        foreach (var endpoint in endpoints.All)
        {
            Add(endpoint);
        }
    }
}