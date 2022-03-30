namespace CustomConsoleATM.Services
{
    public static class ATMInformer
    {
        public static Dictionary<int, int> GetInfoAboutBanknotesAmountsToWithdraw(Dictionary<int, int> banknotesAmounts, Dictionary<int, int> banknotesAmountsToWithdraw)
        {
            Console.WriteLine();
            Console.WriteLine($"Banknotes:");

            var newBanknotesAmounts = new Dictionary<int, int>();

            foreach (var banknotesAmount in banknotesAmounts)
            {
                var banknote = banknotesAmount.Key;
                var amount = banknotesAmount.Value - banknotesAmountsToWithdraw[banknotesAmount.Key];

                if (banknotesAmountsToWithdraw[banknotesAmount.Key] != 0)
                    Console.WriteLine($"${banknote}: {banknotesAmountsToWithdraw[banknotesAmount.Key]}");

                newBanknotesAmounts.Add(banknote, amount);
            }

            Console.WriteLine();
            return newBanknotesAmounts;
        }
    }
}
