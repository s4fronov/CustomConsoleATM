using CustomConsoleATM.Services;

namespace CustomConsoleATM.Centers
{
    public class ATMTransactionCenter
    {
        public int WithdrawalAmount { get; set; }
        public Dictionary<int, int> BanknotesAmountsToWithdraw { get; set; } = new Dictionary<int, int>();

        private ATMStorageCenter StorageCenter { get; set; }
        public int Balance { get; private set; }
        public Dictionary<int, int> BanknotesAmountsInATM { get; private set; }

        public ATMTransactionCenter()
        {
            var _storageCenter = new ATMStorageCenter();
            _storageCenter.ReadStorageState();
            StorageCenter = _storageCenter;
            Balance = _storageCenter.Balance;
            BanknotesAmountsInATM = _storageCenter.BanknotesAmounts;
        }

        public void Withdraw()
        {
            var newBanknotesAmounts = ATMInformer.GetInfoAboutBanknotesAmountsToWithdraw(BanknotesAmountsInATM, BanknotesAmountsToWithdraw);

            StorageCenter.Balance -= WithdrawalAmount;

            Console.WriteLine($"You got: ${WithdrawalAmount}");

            Console.WriteLine();
            Console.WriteLine($"Your balance: ${StorageCenter.Balance}");

            StorageCenter.SaveStorageState(newBanknotesAmounts);
        }
    }
}
