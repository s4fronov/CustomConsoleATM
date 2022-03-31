namespace CustomConsoleATM.Centers
{
    public class ATMTransactionCenter
    {
        public int WithdrawalAmount { get; set; }
        public Dictionary<int, int> BanknotesAmountsToWithdraw { get; set; }

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
            BanknotesAmountsToWithdraw = new Dictionary<int, int>();
        }

        public void Withdraw()
        {
            GetInfoAboutBanknotesAmountsToWithdraw();

            StorageCenter.Balance -= WithdrawalAmount;

            Console.WriteLine($"You got: ${WithdrawalAmount}");

            Console.WriteLine();
            Console.WriteLine($"Your balance: ${StorageCenter.Balance}");

            StorageCenter.SaveStorageState(BanknotesAmountsInATM);
        }

        private void GetInfoAboutBanknotesAmountsToWithdraw()
        {
            Console.WriteLine();
            Console.WriteLine("Banknotes:");

            foreach (var banknote in BanknotesAmountsInATM.Keys.Where(banknote => BanknotesAmountsToWithdraw[banknote] != 0))
            {
                Console.WriteLine($"{banknote}: {BanknotesAmountsToWithdraw[banknote]}");
            }

            Console.WriteLine();
        }
    }
}
