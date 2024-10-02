using System.Collections.Generic;
using System.Collections.ObjectModel;
using FrankThePOSsim.Models;

namespace FrankThePOSsim.observable;

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