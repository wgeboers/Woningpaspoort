using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Woningpaspoort.Model;

namespace Woningpaspoort.ViewModel;

public partial class BaseViewModel : ObservableObject
{
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotBusy))]
	bool isBusy;

	public bool IsNotBusy => !IsBusy;

	//bool isBusy;

	//public bool IsBusy 
	//{ 
	//	get => isBusy;
	//	set
	//	{
	//		if (isBusy == value)
	//			return;
	//		isBusy = value;
	//		OnPropertyChanged();

	//		OnPropertyChanged(nameof(IsNotBusy));
	//	}
	//}

	//public bool IsNotBusy => !isBusy;

	//public event PropertyChangedEventHandler PropertyChanged;

	//public void OnPropertyChanged([CallerMemberName] string name =null) =>
	//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}