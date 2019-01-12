using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BingPage : ContentPage
	{
	    private IBingService _bingService;
		public BingPage ()
		{
			InitializeComponent ();
            _bingService = new BingService();
		}

	    public void OnSearchClicked(object sender, EventArgs args)
	    {
	        var query = EntrySearchQuery.Text;
	        var image = _bingService.GetImage(query);
	        Url.Text = $"Url: {image}";
	        SearchImage.Source = ImageSource.FromUri(new Uri(image));
	    }
	}
}