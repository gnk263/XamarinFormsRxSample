using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [Required(ErrorMessage = "名前を入力してください。")]
        public ReactiveProperty<string> Name { get; }
        public ReadOnlyReactiveProperty<string> NameError { get; }

        [Required(ErrorMessage = "数字を入力してください。")]
        [RegularExpression("[0-9]{1,3}", ErrorMessage = "3桁の数字を入力してください。")]
        public ReactiveProperty<string> Age { get; }
        public ReadOnlyReactiveProperty<string> AgeError { get; }

        public ReactiveCommand CommitCommand { get; }

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


            Name = new ReactiveProperty<string>()
                .SetValidateAttribute(() => Name);
            NameError = Name
                .ObserveErrorChanged
                .Select(x => x?.Cast<string>()?.FirstOrDefault())
                .ToReadOnlyReactiveProperty();

            Age = new ReactiveProperty<string>()
                .SetValidateAttribute((() => Age));
            AgeError = Age
                .ObserveErrorChanged
                .Select(x => x?.Cast<string>()?.FirstOrDefault())
                .ToReadOnlyReactiveProperty();

            CommitCommand = new[]
                {
                    Name.ObserveHasErrors,
                    Age.ObserveHasErrors
                }
                .CombineLatest(x => x.All(y => !y))
                .ToReactiveCommand();
        }
    }
}