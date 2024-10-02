using System.Collections.ObjectModel;
using FrankThePOSsim.Models;

namespace FrankThePOSsim.observable;

internal class TerminalObservable : ObservableCollection<Terminal>
{
    public TerminalObservable(Environment? environment)
    {
        if (environment?.Terminals == null) return;
        foreach (var terminal in environment.Terminals)
        {
            Add(terminal);
        }
    }
}