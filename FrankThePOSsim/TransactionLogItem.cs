using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;

namespace FrankThePOSsim;

public class TransactionLogItem: INotifyPropertyChanged, IDisposable
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
            OnPropertyChanged(nameof(LiveTimestamp));
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
            if (_response != null)
            {
                // Unsubscribe from the old response's PropertyChanged event
                _response.PropertyChanged -= Response_PropertyChanged;
            }
            _stopwatch.Stop();
            _timer.Stop();
            _response = value;
            if (_response != null)
            {
                // Subscribe to the new response's PropertyChanged event
                _response.PropertyChanged += Response_PropertyChanged;
            }
            OnPropertyChanged(nameof(Response));
        }
    }
    public string? Endpoint { get; init; }
    public Transaction? Transaction { get; init; }
    public string? Timestamp { get; init; }
    public string? Url { get; init; }
    public string? Payload { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Response_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Notify that the FullBody property has changed
        if (e.PropertyName == nameof(Response.FullBody))
        {
            OnPropertyChanged(nameof(Response));
        }
    }
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Dispose()
    {
        _timer.Dispose();
        GC.SuppressFinalize(this);
    }
}
