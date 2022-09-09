using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FrankThePOSsim.observable;

internal class CommandObservable : ObservableCollection<string>
{
    public CommandObservable(IEnumerable<string> commands)
    {
        foreach (var command in commands)
        {
            Add(command);
        }
    }
}