using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FrankThePOSsum.observable
{
    internal class EnvironmentObservable : ObservableCollection<Environment>
    {
        public EnvironmentObservable(List<Environment> environments)
        {
            foreach (var environment in environments)
            {
                Add(environment);
            }
        }
    }
}
