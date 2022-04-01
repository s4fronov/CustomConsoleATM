namespace CustomConsoleATM.Centers
{
    public class ATMTransactionCenter
    {
        public int Amount { get; set; }
        private int[] AvailableBanknotes { get; set; }
        private Dictionary<int, int> BanknotesAmountsToWithdraw { get; set; }
        public bool IsError { get; set; }

        private ATMStorageCenter StorageCenter { get; set; }
        public int Balance { get; private set; }
        public Dictionary<int, int> BanknotesAmountsInATM { get; private set; }

        public ATMTransactionCenter()
        {
            var storageCenter = new ATMStorageCenter();
            storageCenter.ReadStorageState();
            StorageCenter = storageCenter;
            Balance = storageCenter.Balance;
            BanknotesAmountsInATM = storageCenter.BanknotesAmounts;

            IsError = false;
            AvailableBanknotes = BanknotesAmountsInATM.Keys.OrderByDescending(x => x).ToArray();
            BanknotesAmountsToWithdraw = AvailableBanknotes.ToDictionary(x => x, x => 0);
        }

        public void Withdraw()
        {
            if (Amount > 0 && Balance >= Amount)
            {
                try
                {
                    GetBanknotesAmountsToWithdraw();
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Amount = 0;
                    IsError = true;
                }
            }
            else
            {
                Console.WriteLine("Entered withdrawal amount is incorrect! Insufficient funds in the account");
                IsError = true;
            }

            StorageCenter.Balance -= Amount;

            Console.WriteLine();
            Console.WriteLine($"You got: ${Amount}");

            Console.WriteLine();
            Console.WriteLine($"Your balance: ${StorageCenter.Balance}");

            StorageCenter.SaveStorageState(BanknotesAmountsInATM);
        }

        private void GetBanknotesAmountsToWithdraw()
        {
            var withdrawAmount = Amount;
            var checkSum = Amount;

            while (withdrawAmount > 0)
            {
                foreach (var banknote in AvailableBanknotes)
                {
                    if (withdrawAmount < banknote)
                        continue;

                    var banknoteAmount = withdrawAmount / banknote;
                    var remainder = withdrawAmount - banknote * banknoteAmount;

                    if (remainder < AvailableBanknotes.Min() &&
                        AvailableBanknotes.Any(x => remainder % x != 0))
                    {
                        banknoteAmount--;
                    }

                    if (BanknotesAmountsInATM[banknote] < banknoteAmount)
                    {
                        banknoteAmount = BanknotesAmountsInATM[banknote];
                    }

                    withdrawAmount -= banknote * banknoteAmount;
                    BanknotesAmountsInATM[banknote] -= banknoteAmount;
                    BanknotesAmountsToWithdraw[banknote] = banknoteAmount;
                }

                if (withdrawAmount == 0 ||
                    checkSum == withdrawAmount ||
                    withdrawAmount < BanknotesAmountsInATM.Keys.Min())
                    break;

                checkSum = withdrawAmount;
            }

            if (withdrawAmount != 0)
            {
                throw new Exception("Error! It's impossible to withdraw such an amount");
            }
            
            Console.WriteLine();
            Console.WriteLine("Banknotes:");

            foreach (var banknote in BanknotesAmountsInATM.Keys.Where(banknote => BanknotesAmountsToWithdraw[banknote] != 0))
            {
                Console.WriteLine($"${banknote}: {BanknotesAmountsToWithdraw[banknote]}");
            }
        }
    }
}
