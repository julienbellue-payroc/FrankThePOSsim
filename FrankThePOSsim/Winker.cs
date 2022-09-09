using System;
using System.Timers;
using System.Windows;

namespace FrankThePOSsim;

public class Winker //Henry
{
    private readonly Random _rnd = new();
    private readonly Timer _timer;
    private readonly int _lowerBoundary;
    private readonly int _upperBoundary;

    public Winker(int lowerBoundary, int upperBoundary)
    {
        _lowerBoundary = lowerBoundary;
        _upperBoundary = upperBoundary;
        
        _timer = new Timer(_rnd.Next(_lowerBoundary, _upperBoundary));
            
        _timer.Elapsed += CloseEye;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void OpenEye(object? source, ElapsedEventArgs e)
    {
        Application.Current.Resources["LeftEye"] = Application.Current.Resources["OpenLeftEye"];
        _timer.Elapsed -= OpenEye;
        _timer.Interval = _rnd.Next(_lowerBoundary, _upperBoundary);
        _timer.Elapsed += CloseEye;
    }

    private void CloseEye(object? source, ElapsedEventArgs e)
    {
        Application.Current.Resources["LeftEye"] = Application.Current.Resources["ClosedLeftEye"];
        _timer.Elapsed -= CloseEye;
        _timer.Interval = 100;
        _timer.Elapsed += OpenEye;
    }
}