namespace CustomConsoleATM.Services
{
    public static class ATMCalculator
    {
        public static Dictionary<int, int> GetBanknotesAmountsToWithdraw(Dictionary<int, int> banknotesAmounts, int withdrawalAmount)
        {
            var banknotesAmountsToWithdraw = new Dictionary<int, int>();

            foreach (var banknote in banknotesAmounts.Keys.OrderByDescending(x => x))
            {
                var count = Math.Min(withdrawalAmount / banknote, banknotesAmounts[banknote]);
                banknotesAmountsToWithdraw[banknote] = count;
                withdrawalAmount -= count * banknote;
            }

            if (withdrawalAmount == 0)
                return banknotesAmountsToWithdraw;
            else
                throw new Exception("Error! It's impossible to withdraw such an amount");
        }
    }
}
