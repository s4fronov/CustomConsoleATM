namespace CustomConsoleATM.Services
{
    public static class ATMCalculator
    {
        public static Dictionary<int, int> GetBanknotesAmountsToWithdraw(Dictionary<int, int> banknotesAmounts, int withdrawalAmount)
        {
            var banknotes = banknotesAmounts.Keys.OrderByDescending(x => x).ToArray();
            var banknotesAmountsToWithdraw = banknotes.ToDictionary(banknote => banknote, banknote => 0);

            while (withdrawalAmount > 0)
            {
                foreach (var banknote in banknotes)
                {
                    if (withdrawalAmount < banknote)
                        continue;

                    var banknoteAmount = withdrawalAmount / banknote;
                    var remainder = withdrawalAmount - banknote * banknoteAmount;

                    if (banknotes.Any(banknoteForCheck => remainder % banknoteForCheck != 0))
                    {
                        banknoteAmount--;
                    }

                    withdrawalAmount -= banknote * banknoteAmount;
                    banknotesAmountsToWithdraw[banknote] = banknoteAmount;
                }

                if (withdrawalAmount == 0)
                    break;

                if (withdrawalAmount < banknotesAmounts.Keys.Min())
                {
                    throw new Exception("Error! It's impossible to withdraw such an amount");
                }
            }



            return banknotesAmountsToWithdraw;
        }
    }
}
