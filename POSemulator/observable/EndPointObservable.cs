using System.Collections.ObjectModel;

namespace FrankThePOSsum.observable
{
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
}
