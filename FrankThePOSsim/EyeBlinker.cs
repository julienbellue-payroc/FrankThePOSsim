using System;
using System.Timers;
using System.Windows;

namespace FrankThePOSsim;

public class EyeBlinker
{
    private readonly Random _rnd = new();
    private readonly Timer _blinkTimer;
    private readonly int _lowerBoundary;
    private readonly int _upperBoundary;

    public EyeBlinker(int lowerBoundary, int upperBoundary)
    {
        _lowerBoundary = lowerBoundary;
        _upperBoundary = upperBoundary;
        
        _blinkTimer = new Timer(_rnd.Next(_lowerBoundary, _upperBoundary));
            
        _blinkTimer.Elapsed += CloseEyes;
        _blinkTimer.AutoReset = true;
        _blinkTimer.Enabled = true;
    }

    private void OpenEyes(object? source, ElapsedEventArgs e)
    {
        Application.Current.Resources["LeftEye"] = Application.Current.Resources["OpenLeftEye"];
        _blinkTimer.Elapsed -= OpenEyes;
        _blinkTimer.Interval = _rnd.Next(_lowerBoundary, _upperBoundary);
        _blinkTimer.Elapsed += CloseEyes;
    }

    private void CloseEyes(object? source, ElapsedEventArgs e)
    {
        Application.Current.Resources["LeftEye"] = Application.Current.Resources["BlinkedLeftEye"];
        _blinkTimer.Elapsed -= CloseEyes;
        _blinkTimer.Interval = 100;
        _blinkTimer.Elapsed += OpenEyes;
    }
}