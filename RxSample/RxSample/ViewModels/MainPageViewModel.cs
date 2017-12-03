﻿using System;
using System.Reactive.Linq;
using Reactive.Bindings;

namespace RxSample.ViewModels
{
    public class MainPageViewModel
    {
        public ReactiveProperty<long> Counter { get; }
        public ReactiveProperty<string> Counter2 { get; }

        public ReactiveProperty<string> Input { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> Output { get; }

        public MainPageViewModel()
        {
            Counter = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .ToReactiveProperty();

            Counter2 = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(x => $"timer is {x} seconds.")
                .ToReactiveProperty();


            Output = Input
                .Delay(TimeSpan.FromSeconds(1))
                .Select(x => x.ToUpper())
                .ToReactiveProperty();
        }
    }
}