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

            foreach (var banknoteAmountGroup in banknotesAmountsMatches.Select(x => x.Groups))
            {
                var banknote = Int32.Parse(banknoteAmountGroup[1].Value);
                var amount = Int32.Parse(banknoteAmountGroup[3].Value);
                BanknotesAmounts.Add(banknote, amount);
            }
        }

        public void SaveStorageState(Dictionary<int, int> newBanknotesAmounts)
        {
            var result = new StringBuilder($"Balance: {Balance}\n\nBanknotes:\n");

            foreach (var (banknote, amount) in newBanknotesAmounts)
            {
                result.Append($"{banknote} - {amount}\n");
            }

            File.WriteAllText(@"..\..\..\BalanceAndBanknotesAmounts.txt", result.ToString());
        }
    }
}
