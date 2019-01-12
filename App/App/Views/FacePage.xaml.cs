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
    public partial class FacePage : ContentPage
    {
        private IFaceService _faceService;
        public FacePage()
        {
            InitializeComponent();
            _faceService = new FaceService();
            
            EntryUrl.Text = @"https://www.wprost.pl/_thumb/e7/11/96ca93e5ae0e1a39e10cbddb5ff9.jpeg";
        }

        public void OnDescribeImageClicked(object sender, EventArgs args)
        {
            string url = EntryUrl.Text;
            var description = _faceService.FaceDetect(url);
            var smile = description.FirstOrDefault().faceAttributes.smile * 100;
	        
            DescriptionText.Text = $"Uśmiech: {smile}%";
            DescribedImage.Source = ImageSource.FromUri(new Uri(url));
        }
    }
}