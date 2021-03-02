using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.AttributeValidation.Attributes;

namespace MicroShopMobile.Models
{
    public class SignUpDto: INotifyPropertyChanged
    {
        private string phoneNumber;
        [EmailAddress("Not a valid Email")]
        [Required("Please enter Email")]
        public string Email { get; set; }
        [Required("Please enter Password")]
        public string Password { get; set; }
        [Required("Please enter User Name")]
        public string UserName { get; set; }
        [Required("Please enter Phone Number")]
        public string PhoneNumber {
            get
            {
                return phoneNumber;

            } set {
                phoneNumber = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(PhoneNumber)));
            } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
