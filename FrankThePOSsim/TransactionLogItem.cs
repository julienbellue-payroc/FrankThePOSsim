using System.ComponentModel;
using System.Diagnostics;
using System.Timers;

namespace FrankThePOSsim;

public class TransactionLogItem: INotifyPropertyChanged
{
    private readonly Timer _timer;
    private string? _liveTimestamp;
    private readonly Stopwatch _stopwatch;
    private TransactionResponse? _response;

    public TransactionLogItem()
    {
        _timer = new Timer(100);
        _timer.Elapsed += (_, _) => UpdateLiveTimestamp();
        _timer.Start();
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }
    
    public string? LiveTimestamp
    {
        get => _liveTimestamp;
        private set
        {
            if (_liveTimestamp == value) return;
            _liveTimestamp = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(LiveTimestamp)));
        }
    }
    private void UpdateLiveTimestamp()
    {
        var elapsed = _stopwatch.Elapsed;
        LiveTimestamp = $"{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds / 100:D1}";
    }
    public TransactionResponse? Response
    {
        get => _response;
        set
        {
            _stopwatch.Stop();
            _timer.Stop();
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
