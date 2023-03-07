using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace Vocappulary.Shared
{
    public class ContextComponent<TViewModel> : ComponentBase where TViewModel : class, INotifyPropertyChanged
    {
        public ContextComponent()
        {
        }

        [Inject]
        protected TViewModel ViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += async (sender, e) =>
                {
                    await InvokeAsync(() =>
                    {
                        StateHasChanged();
                    }
                    );
                };
            }

            await base.OnInitializedAsync();
        }


    }
}

