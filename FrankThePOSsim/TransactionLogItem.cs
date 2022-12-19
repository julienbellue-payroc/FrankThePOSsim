using System.ComponentModel;

namespace FrankThePOSsim;

public class TransactionLogItem: INotifyPropertyChanged
{
    private TransactionResponse? _response;

    public TransactionResponse? Response
    {
        get => _response;
        set
        {
            _response = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Response)));
        }
    }
    public string? Endpoint { get; init; }
    public Transaction? Transaction { get; init; }
    public string? Timestamp { get; init; }
    public string? Url { get; init; }
    public string? Payload { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }
}
