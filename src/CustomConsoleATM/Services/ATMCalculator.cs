namespace CustomConsoleATM.Services
{
    public static class ATMCalculator
    {
        public static Dictionary<int, int> GetBanknotesAmountsToWithdraw(Dictionary<int, int> banknotesAmounts, int withdrawalAmount)
        {
            var banknotes = banknotesAmounts.Keys.OrderByDescending(x => x).ToArray();
            var banknotesAmountsToWithdraw = banknotes.ToDictionary(banknote => banknote, banknote => 0);
            var checkSum = withdrawalAmount;

            while (withdrawalAmount > 0)
            {
                foreach (var banknote in banknotes)
                {
                    if (withdrawalAmount < banknote)
                        continue;

                    var banknoteAmount = withdrawalAmount / banknote;
                    var remainder = withdrawalAmount - banknote * banknoteAmount;

                    if (remainder < banknotes.Min() &&
                        banknotes.Any(x => remainder % x != 0))
                    {
                        banknoteAmount--;
                    }

                    if (banknotesAmounts[banknote] < banknoteAmount)
                    {
                        banknoteAmount = banknotesAmounts[banknote];
                    }

                    withdrawalAmount -= banknote * banknoteAmount;
                    banknotesAmounts[banknote] -= banknoteAmount;
                    banknotesAmountsToWithdraw[banknote] = banknoteAmount;
                }

                if (withdrawalAmount == 0 || 
                    checkSum == withdrawalAmount ||
                    withdrawalAmount < banknotesAmounts.Keys.Min())
                    break;

                checkSum = withdrawalAmount;
            }

            if (withdrawalAmount != 0)
            {
                throw new Exception("Error! It's impossible to withdraw such an amount");
            }

            return banknotesAmountsToWithdraw;
        }
    }
}
