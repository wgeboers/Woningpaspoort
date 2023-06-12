using Woningpaspoort.Model;
using ZXing;

namespace Woningpaspoort.View.Mobile;

public partial class QrPopUpPage
{
	House house;
	public QrPopUpPage(House house)
	{
		InitializeComponent();
		this.house = house;
		houseQrCode.Barcode = house.Code;
    }
}