using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CreditLoanUnoPlatform
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Reset();
        }

        void Reset()
        {
            TextBoxCreditAmount.Text = "";
            TextBoxInterestRate.Text = "";
            TextBoxYears.Text = "";
            TextBoxTotalInterest.Text = "";
            TextBoxTotalToReturn.Text = "";
            TextBoxMonthlyReturn.Text = "";
            TextBlockCreditAmountError.Visibility = Visibility.Collapsed;
            TextBlockInterestRateError.Visibility = Visibility.Collapsed;
            TextBlockYearsError.Visibility = Visibility.Collapsed;
        }

        void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            double creditAmount;
            bool isCreditAmountValid = double.TryParse(TextBoxCreditAmount.Text, out creditAmount);
            double creditAmountReplacement = creditAmount;

            double interestRate;
            bool isInterestRateValid = double.TryParse(TextBoxInterestRate.Text, out interestRate);

            int years;
            bool isYearsValid = int.TryParse(TextBoxYears.Text, out years);

            double sumOfInterest = 0;
            double sumOfInterestReplacement;

            if (!isCreditAmountValid || creditAmount < 0)
            {
                TextBlockCreditAmountError.Visibility = Visibility.Visible;
            }

            if (!isInterestRateValid || interestRate < 0)
            {
                TextBlockInterestRateError.Visibility = Visibility.Visible;
            }

            if (!isYearsValid || years < 0)
            {
                TextBlockYearsError.Visibility = Visibility.Visible;
            }

            if(creditAmount < 0 || interestRate < 0 || years < 0)
            {
                return;
            }

            if(isCreditAmountValid && isInterestRateValid && isYearsValid)
            {
                TextBlockCreditAmountError.Visibility = Visibility.Collapsed;
                TextBlockInterestRateError.Visibility = Visibility.Collapsed;
                TextBlockYearsError.Visibility = Visibility.Collapsed;
                for (int i = 1; i <= years; i++)
                {
                    sumOfInterestReplacement = 0;
                    sumOfInterestReplacement += creditAmountReplacement / 100 * interestRate;
                    creditAmountReplacement -= sumOfInterestReplacement;
                    sumOfInterest += sumOfInterestReplacement;
                }

                TextBoxTotalInterest.Text = sumOfInterest.ToString("N2");
                TextBoxTotalToReturn.Text = (sumOfInterest + creditAmount).ToString("N2");
                TextBoxMonthlyReturn.Text = ((sumOfInterest + creditAmount) / (years * 12)).ToString("N2");
            }
        }
    }
}
