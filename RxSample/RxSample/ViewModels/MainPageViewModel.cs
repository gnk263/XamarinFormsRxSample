using System;
using System.Reactive.Linq;
using Reactive.Bindings;

namespace RxSample.ViewModels
{
    public class MainPageViewModel
    {
        public ReactiveProperty<long> Counter { get; }

        public MainPageViewModel()
        {
            Counter = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .ToReactiveProperty();
        }
    }
}