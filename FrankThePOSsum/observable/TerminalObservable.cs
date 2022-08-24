using System.Collections.ObjectModel;

namespace FrankThePOSsum.observable
{
    internal class TerminalObservable : ObservableCollection<Terminal>
    {
        public TerminalObservable(Environment environment)
        {
            if (environment.Terminals == null) return;
            foreach (var terminal in environment.Terminals)
            {
                Add(terminal);
            }
        }
    }
}
