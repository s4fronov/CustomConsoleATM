using CustomConsoleATM.Centers;
using CustomConsoleATM.Services;

namespace CustomConsoleATM
{
    public class ATM
    {
        private ATMTransactionCenter TransactionCenter { get; set; }

        public ATM()
        {
            var transactionCenter = new ATMTransactionCenter();
            TransactionCenter = transactionCenter;
        }

        public void Withdraw()
        {
            var isError = false;

            Console.WriteLine($"Your balance: ${TransactionCenter.Balance}");

            Console.WriteLine();
            Console.Write("Please enter the withdrawal amount: ");

            TransactionCenter.WithdrawalAmount = Convert.ToInt32(Console.ReadLine());

            if (TransactionCenter.WithdrawalAmount > 0 && TransactionCenter.Balance >= TransactionCenter.WithdrawalAmount)
            {
                try
                {
                    TransactionCenter.BanknotesAmountsToWithdraw =
                        ATMCalculator.GetBanknotesAmountsToWithdraw(
                            TransactionCenter.BanknotesAmountsInATM,
                            TransactionCenter.WithdrawalAmount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    TransactionCenter.WithdrawalAmount = 0;
                    isError = true;
                }
            }
            else
            {
                Console.WriteLine("Entered withdrawal amount is incorrect! Insufficient funds in the account");
                isError = true;
            }

            if (!isError)
                TransactionCenter.Withdraw();

            var status = isError ? "error" : "success";

            Console.WriteLine();
            Console.WriteLine($"Withdrawal status is {status}!");

            Console.ReadKey();
        }
    }
}
