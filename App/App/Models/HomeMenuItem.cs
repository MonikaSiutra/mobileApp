using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public enum MenuItemType
    {
        ComputerVision,
        FaceDetection,
        Browse,
        Bing,
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
