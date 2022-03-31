using System.Text;
using System.Text.RegularExpressions;

namespace CustomConsoleATM.Centers
{
    public class ATMStorageCenter
    {
        public int Balance { get; set; }
        public Dictionary<int, int> BanknotesAmounts { get; set; } = new Dictionary<int, int>();

        public void ReadStorageState()
        {
            var text = File.ReadAllText(@"..\..\..\BalanceAndBanknotesAmounts.txt");
            var balancePattern = @"(Balance: )(\d{1,})";
            var banknoteAmountPattern = @"(\d{1,})( - )(\d{1,})";
            var options = RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled;
            var balanceMatch = Regex.Match(text, balancePattern, options);
            var banknotesAmountsMatches = Regex.Matches(text, banknoteAmountPattern, options);

            Balance = Int32.Parse(balanceMatch.Groups[2].Value);

            foreach (Match banknoteAmountMatch in banknotesAmountsMatches)
            {
                var banknote = Int32.Parse(banknoteAmountMatch.Groups[1].Value);
                var amount = Int32.Parse(banknoteAmountMatch.Groups[3].Value);
                BanknotesAmounts.Add(banknote, amount);
            }
        }

        public void SaveStorageState(Dictionary<int, int> newBanknotesAmounts)
        {
            var result = new StringBuilder($"Balance: {Balance}\n\nBanknotes:\n");

            foreach (var newBanknoteAmount in newBanknotesAmounts)
            {
                result.Append($"{newBanknoteAmount.Key} - {newBanknoteAmount.Value}\n");
            }

            File.WriteAllText(@"..\..\..\BalanceAndBanknotesAmounts.txt", result.ToString());
        }
    }
}
